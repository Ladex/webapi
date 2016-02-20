using System.Net.Http;
using WebApi2Book.Common;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessor;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintainanceProcessing
{
    public interface IAddTaskMaintenanceProcessing
    {
        Task AddTask(NewTask newTask);
    }

    public class AddTaskMaintenanceProcessing : IAddTaskMaintenanceProcessing
    {
        private readonly IAddTaskQueryProcessor _queryProcessor;
        private readonly IAutoMapper _autoMapper;

        public AddTaskMaintenanceProcessing(IAddTaskQueryProcessor queryProcessor, IAutoMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }

        public Task AddTask(NewTask newTask)
        {
            var taskEntity = _autoMapper.Map<Data.Entities.Task>(newTask);

            _queryProcessor.AddTask(taskEntity);

            var task = _autoMapper.Map<Task>(taskEntity);
            //todo: implement link service

            task.AddLink(new Link
            {
                Method = HttpMethod.Get.Method,
                Href = "http://localhos:57175/api/v1/tasks/" + task.TaskId,
                Rel = Constants.CommonLinkRelValues.Self
            });

            return task;
        }
    }
}