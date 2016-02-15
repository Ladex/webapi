using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi2Book.Web.Common
{
    public class UnitWorkActionFilterAttribute : ActionFilterAttribute
    {
        public virtual IActionTransactionHelper ActionTransactionHelper => WebContainerManager.Get<IActionTransactionHelper>();

        public override bool AllowMultiple => false;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ActionTransactionHelper.BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ActionTransactionHelper.EndTransaction(actionExecutedContext);
            ActionTransactionHelper.CloseSession();
        }
    }
}