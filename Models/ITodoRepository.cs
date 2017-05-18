using System.Collections.Generic;

namespace SampleAPI.Models{
    public interface ITodoRepository
    {
        void Add(TodoItem item);
        IList<TodoItem> GetAll();
        TodoItem Find(long key);
        void Remove(long key);
        void Update(TodoItem item);
    }
}