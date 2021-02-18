using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;
using TodoAPI.Repository;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoRepository<Todo> _todoRepo;

        public TodoController(ITodoRepository<Todo> todoRepository)
        {
            _todoRepo = todoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetAll()
        {
            return Ok(_todoRepo.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id)
        {
            var todo = _todoRepo.GetById(id);

            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Todo todo)
        {
            return _todoRepo.Update(id, todo) ? NoContent() : BadRequest();
        }

        [HttpPost]
        public ActionResult Add(Todo todo)
        {
            var result = _todoRepo.Add(todo);

            if (result != null)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var isDeleted = _todoRepo.Delete(id);

            return isDeleted ? NoContent() : NotFound();
        }
    }
}
