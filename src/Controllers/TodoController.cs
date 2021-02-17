using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static readonly List<Todo> todos = new() { new Todo() { Id = 1, IsDone = false, Name = "Done dishes" } };


        [HttpGet]
        public IEnumerable<Todo> GetAll()
        {
            return todos;
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id)
        {
            return todos.FirstOrDefault(x => x.Id == id);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Todo todo)
        {
            var original = todos.FirstOrDefault(x => x.Id == todo.Id);
            if (original != null)
            {
                original.Name = todo.Name;
                original.IsDone = todo.IsDone;
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Add(Todo todo)
        {
            if (todo != null && !string.IsNullOrEmpty(todo.Name))
            {
                if (todos.Select(x => x.Name).Contains(todo.Name))
                {
                    var originTodo = todos.Where(x => x.Name == todo.Name).FirstOrDefault();
                    return CreatedAtAction(nameof(Get), new { id = originTodo.Id }, originTodo);
                }

                var highestId = todos.Count == 0 ? 1 : todos.Select(x => x.Id).Max();
                todo.Id = highestId + 1;
                todos.Add(todo);
                return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var original = todos.FirstOrDefault(x => x.Id == id);
            if (original != null)
            {
                todos.Remove(original);
                return Ok();
            }
            return NotFound();
        }
    }

    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
