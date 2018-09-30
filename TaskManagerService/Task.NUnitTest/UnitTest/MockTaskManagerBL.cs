using Moq;
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
    public class MockTaskManagerBL : ITaskManagerBL
    {
        public Collection<TaskBL> GetTask()
        {
            var data = new Collection<TaskBL>()
            {
                new TaskBL()
                {
                    TaskID = 1,
                    Task = "Task 1",
                       Priority =10,
                       StartDate = DateTime.Now.Date,
                       EndDate = DateTime.Now.AddDays(10),
                       IsActive = true
                },
                new TaskBL(){
                    TaskID = 2,
                    Task = "Task 2",
                       Priority =10,
                       StartDate = DateTime.Now.Date,
                       EndDate = DateTime.Now.AddDays(10),
                       IsActive = true
                },
                new TaskBL(){
                    TaskID = 3,
                    Task = "Task 3",
                       ParentTask = "",
                       Priority =10,
                       StartDate = DateTime.Now.Date,
                       EndDate = DateTime.Now.AddDays(10),
                       IsActive = true
                }
            };

            return data;
        }

        public Collection<string> GetParentTasks(int? taskId = null)
        {
            Collection<TaskBL> tasks = GetTask();
            var data = new Collection<string>();
            tasks.Where(x => (taskId == null) || (x.TaskID != taskId))
                  .Select(t => t.Task).ToList()
                  .ForEach(y => data.Add(y));
            return data;
        }

        public TaskBL GetTaskById(int taskId)
        {
            Collection<TaskBL> tasks = GetTask();
            var data = tasks.Where(x => x.TaskID == taskId)
                .Select(t => new TaskBL()
                {
                    TaskID = t.TaskID,
                    Task = t.Task,
                    ParentTask = t.ParentTask,
                    Priority = t.Priority,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    IsActive = t.IsActive
                }).FirstOrDefault();

            return data;
        }

        public int AddTask(TaskBL task)
        {
            return 0;
        }

        public int UpdateTask(TaskBL task)
        {
            return 0;
        }

        public int EndTask(int taskId)
        {
            return 0;
        }
    }
}
