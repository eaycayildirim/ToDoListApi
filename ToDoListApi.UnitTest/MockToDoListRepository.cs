using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApi.Model;
using ToDoListApi.Repositories;

namespace ToDoListApi.UnitTest
{
    internal class MockToDoListRepository : IToDoListRepository
    {
        public List<ToDoListTask> Tasks{ get; set; } = new List<ToDoListTask>();
    }
}
