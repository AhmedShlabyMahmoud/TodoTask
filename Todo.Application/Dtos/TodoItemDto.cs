using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Dtos
{
    public class TodoItemDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
