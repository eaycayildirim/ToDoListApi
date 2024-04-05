using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Controllers;
using ToDoListApi.Model;
using ToDoListApi.Repositories;

namespace ToDoListApi.UnitTest
{
    [TestClass]
    public class ToDoListControllerTest
    {
        private readonly IToDoListRepository _todoRepository;
        private ToDoListController _controller;

        public ToDoListControllerTest()
        {
            _todoRepository = new MockToDoListRepository();
            _controller = new ToDoListController(_todoRepository);
        }

        [TestMethod]
        public void GetTasks_Returns_Ok()
        {
            //Arrange

            //Act
            var result = _controller.GetTasks();

            //Assert
            Assert.IsInstanceOfType<OkObjectResult>(result.Result);
        }

        [TestMethod]
        public void GetTasks_Returns_All_Tasks()
        {
            //Arrange
            int days = 1;
            _todoRepository.Tasks.Add(new ToDoListTask() { Id = 1, Title="First Task", DueDate=DateTime.Now.AddDays(days), Status = Model.TaskStatus.NotStarted});
            int expectedListItems = 1;

            //Act
            var result = _controller.GetTasks();
            var resultType = result.Result as OkObjectResult;
            var resultList = resultType.Value as List<ToDoListTask>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(resultType.Value, typeof(ToDoListTask));
            Assert.AreEqual(expectedListItems, resultList.Count());
        }
    }
}