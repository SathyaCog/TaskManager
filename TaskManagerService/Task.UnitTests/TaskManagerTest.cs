using TaskManagerService.Controllers;
using TaskManagerService.Models;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System;

namespace Task.UnitTests
{
    [TestClass]
    public class TaskManagerTest
    {
        [TestMethod]
        public void GetTasksTest_Success()
        {
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

        [TestMethod]
        public void GetParentTasksTest_Success()
        {
            var taskController = new TaskController();
            var response = taskController.GetParentTasks(null);
            var responseResult = response as OkNegotiatedContentResult<Collection<string>>;
            Assert.IsNotNull(responseResult);
            Assert.IsNotNull(responseResult.Content);
            foreach (var task in responseResult.Content)
            {
                Assert.IsNotNull(task);
            }
        }

        [TestMethod]
        public void GetTaskByIdTest_Success()
        {
            var taskController = new TaskController();
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

        [TestMethod]
        public void AddTaskTest_Success()
        {
            // Arrange
            TaskController controller = new TaskController();
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

        [TestMethod]
        public void UpdateTaskTest_Success()
        {
            // Arrange
            TaskController controller = new TaskController();
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

        [TestMethod]
        public void EndTaskTest_Success()
        {
            // Arrange
            TaskController controller = new TaskController();

            // Act
            var response = controller.EndTask(1);

            // Assert
            Assert.IsTrue(response is OkResult);
        }

        [TestMethod]
        public void AddTaskTest_Error()
        {
            // Arrange
            TaskController controller = new TaskController();
            TaskModel model = new TaskModel
            {
                Task = "Sample Task",
                ParentTask = "Sample Parent TAsk",
                Priority = 5,
                StartDate = new DateTime(),
                EndDate = new DateTime()
            };

            // Act
            var response = controller.AddTask(model);

            // Assert
            Assert.IsTrue(response is InternalServerErrorResult);
        }

        [TestMethod]
        public void UpdateTaskTest_Error()
        {
            // Arrange
            TaskController controller = new TaskController();
            TaskModel model = new TaskModel
            {
                TaskID = 1,
                Task = "Sample Task",
                ParentTask = "Sample Parent Task",
                Priority = 5,
                StartDate = new DateTime(),
                EndDate = new DateTime()
            };

            // Act
            var response = controller.UpdateTask(model);

            // Assert
            Assert.IsTrue(response is InternalServerErrorResult);
        }
    }
}
