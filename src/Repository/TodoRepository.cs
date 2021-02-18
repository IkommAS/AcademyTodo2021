using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Repository
{
    public class TodoRepository : ITodoRepository<Todo>
    {
        private static readonly List<Todo> todos = new() { new Todo() { Id = 1, IsDone = false, Name = "Dishes" } };

        public IEnumerable<Todo> GetAll()
        {
            return todos;
        }

        public Todo GetById(int id)
        {
            return todos.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(int id, Todo todo)
        {
            var original = todos.FirstOrDefault(x => x.Id == id);
            if (original != null)
            {
                original.Name = todo.Name;
                original.IsDone = todo.IsDone;

                return true;
            }

            return false;
        }

        public Todo Add(Todo todo)
        {
            if (todo != null && !string.IsNullOrEmpty(todo.Name))
            {
                if (todos.Select(x => x.Name).Contains(todo.Name))
                {
                    var originTodo = todos.Where(x => x.Name == todo.Name).FirstOrDefault();

                    return originTodo;
                }
                var highestId = todos.Count == 0 ? 1 : todos.Select(x => x.Id).Max();
                todo.Id = highestId + 1;
                todos.Add(todo);
                return todo;
            }
            return null;
        }

        public bool Delete(int id)
        {
            var original = todos.FirstOrDefault(x => x.Id == id);
            if (original != null)
            {
                todos.Remove(original);
                return true;
            }
            return false;
        }

      
    }
}
