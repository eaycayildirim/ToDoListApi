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
            var resultList = resultType.Value as IEnumerable<ToDoListTask>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(resultType.Value, typeof(IEnumerable<ToDoListTask>));
            Assert.AreEqual(expectedListItems, resultList.Count());
        }

        [TestMethod]
        public void GetTaskById_Returns_Ok()
        {
            //Arrange
            int days = 1;
            int id = 1;
            _todoRepository.Tasks.Add(new ToDoListTask() { Id = id, Title = "First Task", DueDate = DateTime.Now.AddDays(days), Status = Model.TaskStatus.NotStarted });

            //Act
            var result = _controller.GetTaskById(id);

            //Assert
            Assert.IsInstanceOfType<OkObjectResult>(result.Result);

        }

        [TestMethod]
        public void GetTaskById_Returns_Task_With_The_Right_Id()
        {
            //Arrange
            int days = 1;
            int id = 1;
            var task = new ToDoListTask() { Id = id, Title = "First Task", DueDate = DateTime.Now.AddDays(days), Status = Model.TaskStatus.NotStarted };
            _todoRepository.Tasks.Add(task);

            //Act
            var result = _controller.GetTaskById(id);
            var resultType = result.Result as OkObjectResult;
            var resultTask = resultType.Value as ToDoListTask;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(resultType.Value, typeof(ToDoListTask));
            Assert.AreEqual(task, resultTask);
        }

        [TestMethod]
        public void CreateTask_Returns_CreatedAtRoute()
        {
            //Arrange
            int days = 1;
            int id = 1;
            var task = new ToDoListTask() { Id = id, Title = "First Task", DueDate = DateTime.Now.AddDays(days), Status = Model.TaskStatus.NotStarted };


            //Act
            var result = _controller.CreateTask(task);

            //Assert
            Assert.IsInstanceOfType<CreatedAtRouteResult>(result.Result);
        }

        [TestMethod]
        public void CreateTask_Returns_New_Created_Task()
        {
            //Arrange
            int days = 1;
            int id = 1;
            var task = new ToDoListTask() { Id = id, Title = "First Task", DueDate = DateTime.Now.AddDays(days), Status = Model.TaskStatus.NotStarted };

            //Act
            var result = _controller.CreateTask(task);
            var resultType = result.Result as CreatedAtRouteResult;
            var resultTask = resultType.Value as ToDoListTask;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(resultType.Value, typeof(ToDoListTask));
            Assert.AreEqual(task, resultTask);
        }

        [TestMethod]
        public void UpdateTask_Returns_NoContent()
        {
            //Arrange
            int days = 1;
            int id = 1;
            var task = new ToDoListTask() { Id = id, Title = "First Task", DueDate = DateTime.Now.AddDays(days), Status = Model.TaskStatus.NotStarted };
            _controller.CreateTask(task);
            var updatedTask = new ToDoListTask() { Id = id, Title = "First Task", DueDate = DateTime.Now.AddDays(days), Status = Model.TaskStatus.Finished };

            //Act
            var result = _controller.UpdateTask(1, task);

            //Assert
            Assert.IsInstanceOfType<NoContentResult>(result);
        }
    }
}