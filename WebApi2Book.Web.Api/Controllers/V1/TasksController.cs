using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [UnitOfWorkActionFilter]
    [ApiVersion1RoutePrefix("tasks")]
    public class TasksController : ApiController
    {
        [HttpPost]
        [Route("", Name = "AddTaskRoute")]
        public Task AddTask(HttpRequestMessage requestMessage, NewTask newTask)
        {
            return new Task
            {
                Subject = "In v1, newTask.subject = " + newTask.Subject
            };
        }
    }
}
