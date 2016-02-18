using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.QueryProcessor
{
    public interface IAddTaskQueryProcessor
    {
        void AddTask(Task task);
    }
}