using ToDoListApi.Model;

namespace ToDoListApi.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        public List<ToDoListTask> Tasks { get; set; } = new List<ToDoListTask>();
    }
}
