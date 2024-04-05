using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Model;
using ToDoListApi.Repositories;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository _todoRepository;

        public ToDoListController(IToDoListRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ToDoListTask>> GetTasks()
        {
            return Ok(_todoRepository.Tasks);
        }

        [HttpGet("{id:int}", Name = "GetTaskById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ToDoListTask>> GetTaskById(int id)
        {
            var task = _todoRepository.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return NotFound($"Task with id: {id} not found");
            return Ok(task);
        }
    }
}
