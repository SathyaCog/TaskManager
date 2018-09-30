using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http;
using Task.BusinessLayer;
using TaskManagerService.Models;

namespace TaskManagerService.Controllers
{
    [RoutePrefix("api/Task")]
    public class TaskController : ApiController
    {
        private readonly ITaskManagerBL _taskManagerBL = null;

        public TaskController()
        {
            _taskManagerBL = new TaskManagerBL();
        }

        public TaskController(ITaskManagerBL taskBL)
        {
            _taskManagerBL = taskBL;
        }

        [HttpGet]
        [Route("GetTasks")]
        public IHttpActionResult GetTasks()
        {
            Collection<TaskModel> tasks = new Collection<TaskModel>();

            var blTasks = _taskManagerBL.GetTask();
            blTasks.ToList().ForEach(
               x => tasks.Add(
                   new TaskModel
                   {
                       TaskID = x.TaskID,
                       Task = x.Task,
                       ParentTask = x.ParentTask ?? "",
                       Priority = x.Priority,
                       StartDate = x.StartDate,
                       EndDate = x.EndDate,
                       IsActive = x.IsActive
                   }));

            return Ok(tasks);

        }

        [HttpGet]
        [Route("GetParentTasks/{taskId}")]
        public IHttpActionResult GetParentTasks(int? taskId = null)
        {
            Collection<string> tasks = new Collection<string>();

            var blTasks = _taskManagerBL.GetParentTasks(taskId);
            blTasks.ToList().ForEach(x => tasks.Add(x));

            return Ok(tasks);

        }

        [HttpGet]
        [Route("GetTasks/{taskId}")]
        public IHttpActionResult GetTaskById(int taskId)
        {
            TaskModel task = new TaskModel();

            var blTasks = _taskManagerBL.GetTaskById(taskId);
            if (blTasks != null)
            {
                task = new TaskModel
                {
                    TaskID = blTasks.TaskID,
                    Task = blTasks.Task,
                    ParentTask = blTasks.ParentTask,
                    Priority = blTasks.Priority,
                    StartDate = blTasks.StartDate,
                    EndDate = blTasks.EndDate,
                    IsActive = blTasks.IsActive
                };
            }

            return Ok(task);
        }

        [HttpPost]
        [Route("AddTask")]
        public IHttpActionResult AddTask([FromBody]TaskModel task)
        {
            try
            {
                TaskBL blTask = new TaskBL
                {
                    Task = task.Task,
                    ParentTask = task.ParentTask,
                    Priority = task.Priority,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate
                };
                int result = _taskManagerBL.AddTask(blTask);
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("UpdateTask")]
        public IHttpActionResult UpdateTask([FromBody]TaskModel task)
        {
            try
            {
                TaskBL blTask = new TaskBL
                {
                    TaskID = task.TaskID,
                    ParentTask = task.ParentTask,
                    Priority = task.Priority,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate
                };

                int result = _taskManagerBL.UpdateTask(blTask);
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("EndTask")]
        public IHttpActionResult EndTask([FromBody]int taskId)
        {
            int result = _taskManagerBL.EndTask(taskId);
            return Ok();
        }
    }
}