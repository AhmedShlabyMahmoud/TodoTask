using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Dtos;
using Todo.Application.IService;
using Todo.Domain.Entities;
using Todo.Infrastructure.Context;


namespace Todo.Application.Service
{
    public class TodoService(ApplicationDbContext _context) : ITodoService
    {

        
        public async Task<TodoItem> CreateTodoAsync(TodoItemDto dto)
        {
            var todo = new TodoItem
            {
                Title = dto.Title,
                Description = dto.Description
            };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();


            return todo;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await Task.FromResult(_context.Todos.ToList());

        }

        public async Task<IEnumerable<TodoItem>> GetPendingTodosAsync()
        {
            return await Task.FromResult(_context.Todos.Where(t => !t.IsCompleted).ToList());
        }

        public async Task<bool> MarkAsCompletedAsync(int id)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return false;

            todo.IsCompleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
