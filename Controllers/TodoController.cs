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
        private static readonly Todo[] todos = new Todo[] { new Todo() { Id = 1, IsDone = false, Name = "Done dishes" } };


        [HttpGet]
        public IEnumerable<Todo> GetAll()
        {
            return todos;
        }

        [HttpGet("{id}")]
        public Todo Get(int id)
        {
            return todos.FirstOrDefault(x=> x.Id == id);
        }

        [HttpPut("{id}")]
        public void Update(Todo todo)
        {
            //update
        }

        [HttpPost]
        public void Add(Todo todo)
        {
            //Add
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //delete
        }
    }

    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
    }
}
