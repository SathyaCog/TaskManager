using System;
using System.Collections.ObjectModel;
using System.Linq;
using Task.DataAccessLayer;

namespace Task.BusinessLayer
{
    public class TaskManagerBL : ITaskManagerBL
    {
        private readonly TaskManagerEntities _taskManager;

        public TaskManagerBL()
        {
            _taskManager = new TaskManagerEntities();
        }

        public TaskManagerBL(TaskManagerEntities taskManager)
        {
            _taskManager = taskManager;
        }

        public Collection<TaskBL> GetTask()
        {

            Collection<TaskBL> taskCollection = new Collection<TaskBL>();
            _taskManager.tblTasks
               .Select(t => new TaskBL()
               {
                   TaskID = t.TaskID,
                   Task = t.Task,
                   ParentTask = t.ParentTask,
                   Priority = t.Priority,
                   StartDate = t.StartDate,
                   EndDate = t.EndDate,
                   IsActive = t.IsActive
               })
               .ToList()
               .ForEach(y => taskCollection.Add(y));
            return taskCollection;
        }

        public Collection<string> GetParentTasks(int? taskId = null)
        {

            Collection<string> taskCollection = new Collection<string>();

            _taskManager.tblTasks
                    .Where(x => (taskId == null) || (x.TaskID != taskId))
                   .Select(t => t.Task).ToList()
                   .ForEach(y => taskCollection.Add(y));

            return taskCollection;

        }

        public TaskBL GetTaskById(int taskId)
        {

            TaskBL task = new TaskBL();
            task = _taskManager.tblTasks
                .Where(x => x.TaskID == taskId)
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

            return task;

        }

        public int AddTask(TaskBL task)
        {
            tblTask tTask = new tblTask
            {
                Task = task.Task,
                ParentTask = task.ParentTask,
                Priority = task.Priority,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                IsActive = true
            };

            _taskManager.tblTasks.Add(tTask);
            int result = _taskManager.SaveChanges();
            return result;
        }

        public int UpdateTask(TaskBL task)
        {
            int result = -1;
            var tTask = _taskManager.tblTasks.Where(t => t.TaskID == task.TaskID).FirstOrDefault();
            if (tTask != null)
            {
                tTask.ParentTask = task.ParentTask;
                tTask.Priority = task.Priority;
                tTask.StartDate = task.StartDate;
                tTask.EndDate = task.EndDate;
                result = _taskManager.SaveChanges();
            }
            return result;
        }

        public int EndTask(int taskId)
        {
            int result = -1;
            var task = _taskManager.tblTasks.Where(t => t.TaskID == taskId).FirstOrDefault();
            if (task != null)
            {
                task.EndDate = DateTime.Now;
                task.IsActive = false;
                result = _taskManager.SaveChanges();
            }
            return result;
        }
    }

    public interface ITaskManagerBL
    {
        Collection<TaskBL> GetTask();
        Collection<string> GetParentTasks(int? taskId);
        TaskBL GetTaskById(int taskId);
        int AddTask(TaskBL task);
        int UpdateTask(TaskBL task);
        int EndTask(int taskId);
    }

    public class TaskBL
    {
        public int TaskID { get; set; }
        public string Task { get; set; }
        public string ParentTask { get; set; }
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class SearchTask
    {
        public string Task { get; set; }
        public string ParentTask { get; set; }
        public int? PriorityFrom { get; set; }
        public int? PriorityTo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
