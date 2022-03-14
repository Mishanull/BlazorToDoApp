using Domain.Models;

namespace FileData.DataAccess;

public class ToDoFileDAO : IToDoHome
{
    private FileContext fileContext;

    public ToDoFileDAO(FileContext fileContext)
    {
        this.fileContext = fileContext;
    }
    public Task<ICollection<Todo>> GetAsync()
    {
        ICollection<Todo> todos = fileContext.Todos;
        return  Task.FromResult(todos);
    }

    public Task<Todo> GetById(int id)
    {
        return Task.FromResult(fileContext.Todos.First(t => t.Id == id));    }
    
    public async Task<Todo> AddAsync(Todo todo)
    {
        int largestId = fileContext.Todos.Max(t => t.Id);
        int nextId = largestId + 1;
        todo.Id = nextId;
        fileContext.Todos.Add(todo);
        fileContext.SaveChanges();
        return todo;
    }


    public async Task DeleteAsync(int id)
    {
        Todo t = fileContext.Todos.First(t => t.Id == id);
        fileContext.Todos.Remove(t);
        fileContext.SaveChanges();
       
    }

    public Task UpdateAsync(Todo todo)
    {
        Todo toUpdate = fileContext.Todos.First(t => t.Id == todo.Id);
        toUpdate.IsCompleted = todo.IsCompleted;
        toUpdate.OwnerId = todo.OwnerId;
        toUpdate.Title = todo.Title;
        fileContext.SaveChanges();
        return Task.CompletedTask;
    }
    
}