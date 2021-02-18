using System;
using System.Collections.Generic;
using TodoAPI.Models;

namespace TodoAPI.Repository
{
    public interface ITodoRepository
    {
        Tuple<bool, Todo> Add(Todo todo);
        bool Delete(int id);
        IEnumerable<Todo> GetAll();
        Todo GetById(int id);
        bool Update(int id, Todo todo);
    }
}