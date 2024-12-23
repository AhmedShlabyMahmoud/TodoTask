using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Dtos;
using Todo.Application.IService;
using Todo.Application.Service;

namespace Todo.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService _todoService, ILogger<TodoService> _logger) : ControllerBase
    {


        /// <summary>
        /// Add New Todo Item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItemDto dto)
        {
            var result = await _todoService.CreateTodoAsync(dto);
            _logger.LogInformation("Todo created with ID {Id}", result.Id);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
        /// <summary>
        /// Get All Todo List
        /// </summary>
        /// <returns> Todo List </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }
        /// <summary>
        /// Get all Tast not completed
        /// </summary>
        /// <returns></returns>
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var todos = await _todoService.GetPendingTodosAsync();
            return Ok(todos);
        }
        /// <summary>
        /// mark task to Complete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var result = await _todoService.MarkAsCompletedAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
