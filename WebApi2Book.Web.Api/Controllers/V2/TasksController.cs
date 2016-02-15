﻿using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;

namespace WebApi2Book.Web.Api.Controllers.V2
{
    [UnitOfWorkActionFilter]
    [RoutePrefix("api/{apiVersion:apiVersionConstraint(v2)}/tasks")]
    public class TasksController : ApiController
    {
        [HttpPost]
        [Route("", Name = "AddTaskRouteV2")]
        public Task AddTask(HttpRequestMessage requestMessage, Task newTask)
        {
            return new Task
            {
                Subject = "In v2, newTask.subject = " + newTask.Subject
            };
        }
    }
}
