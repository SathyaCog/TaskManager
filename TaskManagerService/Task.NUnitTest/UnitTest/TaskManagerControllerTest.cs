using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using TaskManagerService.Controllers;
using TaskManagerService.Models;
using System.Web.Http.Results;
using Moq;
using Task.BusinessLayer;
using System.Linq;
using System.Data.Entity;

namespace Task.NUnitTest.UnitTest
{
    [TestFixture]
    public class TaskManagerControllerTest
    {
        [Test]
        public void GetTasksTest_Success()
        {
            ITaskManagerBL taskBL = new MockTaskManagerBL();

            var taskController = new TaskController();
            var response = taskController.GetTasks();
            var responseResult = response as OkNegotiatedContentResult<Collection<TaskModel>>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            foreach (var task in responseResult.Content)
            {
                Assert.IsNotNull(task.TaskID);
                Assert.IsNotNull(task.Task);
                Assert.IsNotNull(task.Priority);
                Assert.IsNotNull(task.StartDate);
                Assert.IsNotNull(task.EndDate);
            }
        }

        [Test]
        public void GetParentTasksTest_Success()
        {
            ITaskManagerBL taskBL = new MockTaskManagerBL();
            var taskController = new TaskController(taskBL);
            var response = taskController.GetParentTasks();
            var responseResult = response as OkNegotiatedContentResult<Collection<string>>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            foreach (var task in responseResult.Content)
            {
                Assert.IsNotNull(task);
            }
        }

        [Test]
        public void GetTaskByIdTest_Success()
        {
            ITaskManagerBL taskBL = new MockTaskManagerBL();
            var taskController = new TaskController(taskBL);
            var response = taskController.GetTaskById(1);
            var responseResult = response as OkNegotiatedContentResult<TaskModel>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            Assert.AreEqual(1, responseResult.Content.TaskID);
            Assert.IsNotNull(responseResult.Content.Task);
            Assert.IsNotNull(responseResult.Content.Priority);
            Assert.IsNotNull(responseResult.Content.StartDate);
            Assert.IsNotNull(responseResult.Content.EndDate);
        }

        [Test]
        public void AddTaskTest_Success()
        {
            ITaskManagerBL taskBL = new MockTaskManagerBL();
            // Arrange
            TaskController controller = new TaskController(taskBL);
            TaskModel model = new TaskModel
            {
                Task = "Sample Task",
                ParentTask = "Sample Parent TAsk",
                Priority = 5,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(10)
            };

            // Act
            var response = controller.AddTask(model);

            // Assert
            Assert.IsTrue(response is OkResult);
        }

        [Test]
        public void UpdateTaskTest_Success()
        {
            ITaskManagerBL taskBL = new MockTaskManagerBL();
            // Arrange
            TaskController controller = new TaskController(taskBL);
            TaskModel model = new TaskModel
            {
                TaskID = 1,
                Task = "Sample Task",
                ParentTask = "Sample Parent Task",
                Priority = 5,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(10)
            };

            // Act
            var response = controller.UpdateTask(model);

            // Assert
            Assert.IsTrue(response is OkResult);
        }

        [Test]
        public void EndTaskTest_Success()
        {
            ITaskManagerBL taskBL = new MockTaskManagerBL();
            // Arrange
            TaskController controller = new TaskController(taskBL);

            // Act
            var response = controller.EndTask(1);

            // Assert
            Assert.IsTrue(response is OkResult);
        }
    }
}
