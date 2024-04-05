using ToDoListApi.Model;

namespace ToDoListApi.Repositories
{
    public interface IToDoListRepository
    {
        List<ToDoListTask> Tasks { get; set; }
    }
}
