using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class ToDosController : ControllerBase
{
    private IToDoHome _toDoHome;
    public ToDosController(IToDoHome todoHome)
    {
       _toDoHome = todoHome;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Todo>>> GetAll()
    {
        try
        {
            ICollection<Todo> todos = await _toDoHome.GetAsync();
            return Ok(todos);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpPost]
    public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
    {
        try
        {
            Todo added = await _toDoHome.AddAsync(todo);
            return Created($"/todos/{added.Id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<String>> DeleteToDo([FromRoute] int id)
    {
        try
        {
            
             await _toDoHome.DeleteAsync(id);
            return Ok("Todo deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Todo>> GetById([FromRoute] int id)
    {
        try
        {
            Todo get=await _toDoHome.GetById(id);
            return Ok(get);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult<Todo>> Update([FromBody] Todo updated)
    {
        try
        {
            await _toDoHome.UpdateAsync(updated);
            return Ok(updated);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

}