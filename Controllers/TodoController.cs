using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;

namespace SampleAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class TodoController: Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public IList<TodoItem> Get()
        {
            return _todoRepository.GetAll();
        }

        [HttpGet]
        [Route("{id:long}", Name="GetTodo")]
        public IActionResult Get(long id)
        {
            var item = _todoRepository.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem item)
        {
            if(item == null)
            {
                return BadRequest();
            }

            _todoRepository.Add(item);

            return CreatedAtRoute("GetTodo", new { id = item.Key}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id,[FromBody] TodoItem item)
        {
            if(item == null || item.Key!=id)
            {
                return BadRequest();
            }
            var todo = _todoRepository.Find(id);
            if(todo == null)
            {
                return NotFound();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _todoRepository.Update(todo);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _todoRepository.Find(id);
            if(todo == null)
            {
                return NotFound();
            }

            _todoRepository.Remove(id);
            return new NoContentResult();
        }
    }
}