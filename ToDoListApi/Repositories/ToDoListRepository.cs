using ToDoListApi.Model;

namespace ToDoListApi.Repositories
{
    /// <summary>
    /// Repository to manage the list of tasks
    /// </summary>
    public class ToDoListRepository : IToDoListRepository
    {
        /// <summary>
        /// The collection of the tasks
        /// </summary>
        public List<ToDoListTask> Tasks { get; set; } = new List<ToDoListTask>();
    }
}
