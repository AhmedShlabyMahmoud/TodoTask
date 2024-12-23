using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Dtos;
using Todo.Domain.Entities;

namespace Todo.Application.IService
{
    public interface ITodoService
    {
        //    Create a new Todo item. Done
        //Mark a Todo item as completed. Done 
        // Retrieve all Todo items.  Done 
        // Retrieve pending (not completed) Todo items.  Done

        Task<TodoItem> CreateTodoAsync(TodoItemDto todoItemDto);
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<IEnumerable<TodoItem>> GetPendingTodosAsync();
        Task<bool> MarkAsCompletedAsync(int id);
    }
}
