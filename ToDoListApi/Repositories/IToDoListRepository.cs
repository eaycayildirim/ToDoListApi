using ToDoListApi.Model;

namespace ToDoListApi.Repositories
{
    /// <summary>
    /// Interface of the ToDoListRepository
    /// </summary>
    public interface IToDoListRepository
    {
        List<ToDoListTask> Tasks { get; set; }
    }
}
