using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Dtos;
using Todo.Application.IService;
using Todo.Application.Service;
using Todo.Domain.Entities;
using Todo.Infrastructure.Context;

namespace TodoApp.Tests
{
    public class TodoServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ITodoService _service;

        public TodoServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new TodoService(_context);
        }
        [Fact]
        public async Task CreateTodoAsync_ShouldAddTodoItem()
        {
            // Arrange
            var dto = new TodoItemDto { Title = "Test Todo", Description = "Test Description" };

            // Act
            var result = await _service.CreateTodoAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Todo", result.Title);
            Assert.Equal("Test Description", result.Description);
            Assert.False(result.IsCompleted);
            Assert.Single(_context.Todos);
        }
        [Fact]
        public async Task GetAllTodosAsync_ShouldReturnAllTodos()
        {
          
            _context.Todos.Add(new TodoItem { Title = "Todo 1" });
            _context.Todos.Add(new TodoItem { Title = "Todo 2" });
            await _context.SaveChangesAsync();

            
            var result = await _service.GetAllTodosAsync();

          
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task GetPendingTodosAsync_ShouldReturnOnlyPendingTodos()
        {

            _context.Todos.Add(new TodoItem { Title = "Todo 1", IsCompleted = false });
            _context.Todos.Add(new TodoItem { Title = "Todo 2", IsCompleted = true });
            await _context.SaveChangesAsync();


            var result = await _service.GetPendingTodosAsync();


            Assert.Single(result);
            Assert.False(result.First().IsCompleted);
        }

        [Fact]
        public async Task MarkAsCompletedAsync_ShouldMarkTodoAsCompleted()
        {
            var todo = new TodoItem { Title = "Todo 1", IsCompleted = false };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            var result = await _service.MarkAsCompletedAsync(todo.Id);

        
            Assert.True(result);
            var updatedTodo = _context.Todos.FirstOrDefault(t => t.Id == todo.Id);
            Assert.NotNull(updatedTodo);
            Assert.True(updatedTodo.IsCompleted);
        }
        [Fact]
        public async Task MarkAsCompletedAsync_ShouldReturnFalseIfTodoNotFound()
        {
     
            var result = await _service.MarkAsCompletedAsync(999); // Invalid ID

      
            Assert.False(result);
        }
    }
}
