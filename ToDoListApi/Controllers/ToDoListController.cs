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
    }
}
