using System;
using System.Collections.Generic;
using TodoAPI.Models;

namespace TodoAPI.Repository
{
    public interface ITodoRepository<T> where T : Todo
    {
        T Add(T todo);
        bool Delete(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Update(int id, T todo);
    }
}