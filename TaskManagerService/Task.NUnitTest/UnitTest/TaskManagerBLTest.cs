using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.BusinessLayer;
using Task.DataAccessLayer;

namespace Task.NUnitTest.UnitTest
{
    [TestFixture]
    public class TaskManagerBLTest
    {
        [Test]
        public void GetTaskTestBL()
        {
            Mock<TaskManagerEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskManagerBL(mockContext.Object);
            Collection<TaskBL> tasks = taskManagerBL.GetTask();
            Assert.IsNotNull(tasks);
            foreach (var task in tasks)
            {
                Assert.IsNotNull(task.TaskID);
                Assert.IsNotNull(task.Task);
                Assert.IsNotNull(task.Priority);
                Assert.IsNotNull(task.StartDate);
                Assert.IsNotNull(task.EndDate);
            }
        }

        [Test]
        public void GetParentTasksTest_WIthID()
        {
            Mock<TaskManagerEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskManagerBL(mockContext.Object);
            Collection<string> tasks = taskManagerBL.GetParentTasks(1);
            Assert.IsNotNull(tasks);
            foreach (var task in tasks)
            {
                Assert.AreNotEqual("Task 1", task);
            }
        }

        [Test]
        public void GetParentTasksTest_WithoutID()
        {
            Mock<TaskManagerEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskManagerBL(mockContext.Object);
            Collection<string> tasks = taskManagerBL.GetParentTasks();
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count > 0);
        }

        [Test]
        public void GetTaskByIdTest()
        {
            Mock<TaskManagerEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskManagerBL(mockContext.Object);
            var task = taskManagerBL.GetTaskById(1);
            Assert.IsNotNull(task);
            Assert.AreEqual(1, task.TaskID);
        }

        [Test]
        public void AddTaskTest()
        {
            Mock<TaskManagerEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskManagerBL(mockContext.Object);
            TaskBL model = new TaskBL
            {
                Task = "Task 4",
                ParentTask = "Task 1",
                Priority = 5,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(10)
            };

            int result = taskManagerBL.AddTask(model);

            Assert.IsTrue(result == 0);
        }

        [Test]
        public void UpdateTaskTest()
        {
            Mock<TaskManagerEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskManagerBL(mockContext.Object);
            TaskBL model = new TaskBL
            {
                TaskID = 1,
                Task = "Task 5",
                ParentTask = "Task 2",
                Priority = 5,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(10)
            };

            int result = taskManagerBL.UpdateTask(model);

            Assert.IsTrue(result == 0);
        }

        [Test]
        public void EndTaskTest()
        {
            Mock<TaskManagerEntities> mockContext = MockDataSetList();
            var taskManagerBL = new TaskManagerBL(mockContext.Object);
            int result = taskManagerBL.EndTask(1);

            Assert.IsTrue(result == 0);
        }

        private static Mock<TaskManagerEntities> MockDataSetList()
        {
            var data = new List<tblTask>()
            {
               new tblTask()
                {
                    TaskID = 1,
                    Task = "Task 1",
                       ParentTask = "",
                       Priority =10,
                       StartDate = DateTime.Now.Date,
                       EndDate = DateTime.Now.AddDays(10),
                       IsActive = true
                },
                new tblTask()
                {
                    TaskID = 2,
                    Task = "Task 2",
                       ParentTask = "",
                       Priority =10,
                       StartDate = DateTime.Now.Date,
                       EndDate = DateTime.Now.AddDays(10),
                       IsActive = true
                },
                new tblTask()
                {
                    TaskID = 3,
                    Task = "Task 3",
                       ParentTask = "",
                       Priority =10,
                       StartDate = DateTime.Now.Date,
                       EndDate = DateTime.Now.AddDays(10),
                       IsActive = true
                }

            }.AsQueryable();

            var mockset = new Mock<DbSet<tblTask>>();
            mockset.As<IQueryable<tblTask>>().Setup(m => m.Provider).Returns(data.Provider);
            mockset.As<IQueryable<tblTask>>().Setup(m => m.Expression).Returns(data.Expression);
            mockset.As<IQueryable<tblTask>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockset.As<IQueryable<tblTask>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<TaskManagerEntities>();
            mockContext.Setup(m => m.tblTasks).Returns(mockset.Object);

            return mockContext;
        }
    }
}
