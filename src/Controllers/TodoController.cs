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
        private ITodoRepository _todoRepo;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepo = todoRepository;
        }

        [HttpGet]
        public IEnumerable<Todo> GetAll()
        {
            return _todoRepo.GetAll();
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
            return _todoRepo.Update(id, todo) ? Ok() : BadRequest();
        }

        [HttpPost]
        public ActionResult Add(Todo todo)
        {
            var result = _todoRepo.Add(todo);

            if (result.Item1)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Item2.Id }, result.Item2);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var isDeleted = _todoRepo.Delete(id);

            return isDeleted ? Ok() : NotFound();
        }
    }


}
