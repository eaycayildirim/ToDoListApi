using ToDoListApi.Model;
using ToDoListApi.Repositories;

namespace ToDoListApi.UnitTest
{
    internal class MockToDoListRepository : IToDoListRepository
    {
        public List<ToDoListTask> Tasks{ get; set; } = new List<ToDoListTask>();
    }
}
