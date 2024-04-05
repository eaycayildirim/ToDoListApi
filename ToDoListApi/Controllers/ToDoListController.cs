using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Model;
using ToDoListApi.Repositories;

namespace ToDoListApi.Controllers
{
    /// <summary>
    /// Controller to send HTTP requests to the API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository _todoRepository;

        public ToDoListController(IToDoListRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        /// <summary>
        /// Retreives the list of tasks.
        /// </summary>
        /// <returns>Returns a list of tasks</returns>
        /// <response code="200">Returns ok if list is found</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ToDoListTask>> GetTasks()
        {
            return Ok(_todoRepository.Tasks);
        }

        /// <summary>
        /// Retreives a task by its ID.
        /// </summary>
        /// <param name="id">ID number of the task</param>/>
        /// <returns>Returns a single task with specified ID</returns>
        /// <response code="200">Returns ok if task is found</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item is not found</response>
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

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">Task inputs to be created</param>/>
        /// <returns>Returns the new created task</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item is not found</response>

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

        /// <summary>
        /// Updates a new task.
        /// </summary>
        /// <param name="id">ID number of the task to be updated</param>/>
        /// <param name="task">Task inputs of the task to be updated</param>/>
        /// <returns>Returns no content if successful, otherwise a bad request or not found</returns>
        /// <response code="204">Returns no content if task updated</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item is not found</response>
        [HttpPut("Update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTask(int id, [FromBody] ToDoListTask task)
        {
            if(task == null) 
                return BadRequest("Invalid Task");
            if (id != task.Id)
                return BadRequest("Id does not match the Id from requested body.");
            var existedTask = _todoRepository.Tasks.FirstOrDefault(x => x.Id == task.Id);
            if(existedTask == null)
                return NotFound($"Task with id: {id} not found");

            existedTask.Title = task.Title;
            existedTask.Description = task.Description;
            existedTask.DueDate = task.DueDate;
            existedTask.Status = task.Status;
            return NoContent();
        }

        /// <summary>
        /// Deletes a task.
        /// </summary>
        /// <param name="id">ID number of the task to be updated</param>/>
        /// <returns>Returns ok if successful, otherwise a bad request or not found</returns>
        /// <response code="200">Returns ok if task is deleted</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item is not found</response>
        [HttpDelete("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTask(int id)
        {
            var task = _todoRepository.Tasks.FirstOrDefault(x => x.Id == id);
            if(task == null)
                return NotFound($"Task with id: {id} not found");
            _todoRepository.Tasks.Remove(task);
            return Ok();
        }
    }
}
