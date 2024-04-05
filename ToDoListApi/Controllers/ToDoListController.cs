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
        public ActionResult<ToDoListTask> GetTaskById(int id)
        {
            var task = _todoRepository.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return NotFound($"Task with id: {id} not found");
            return Ok(task);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ToDoListTask> CreateTask([FromBody]ToDoListTask task)
        {
            if (task == null)
                return BadRequest(task);
            if(_todoRepository.Tasks.Any(x => x.Title.ToLower() == task.Title.ToLower()))
            {
                ModelState.AddModelError("TitleError", "Title already exists.");
                return BadRequest(ModelState);
            }
            if(_todoRepository.Tasks.Any(x => x.Id == task.Id))
            {
                ModelState.AddModelError("IdError", "Id already exists.");
                return BadRequest(ModelState);
            }
            int newId = _todoRepository.Tasks.Count() > 0 ? _todoRepository.Tasks.Max(x => x.Id) + 1 : 1;
            task.Id = newId;
            _todoRepository.Tasks.Add(task);
            return CreatedAtRoute("GetTaskById", new {id = task.Id}, task);
        }
    }
}
