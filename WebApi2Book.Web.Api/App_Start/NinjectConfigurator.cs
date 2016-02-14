using log4net.Config;
using Ninject;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;

namespace WebApi2Book.Web.Api
{
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBinding(container);
        }

        private static void AddBinding(IKernel container)
        {
            ConfigureLog4Net(container);
            container.Bind<IDateTime>().To<DateTImeAdapter>().InSingletonScope();
        }

        private static void ConfigureLog4Net(IKernel container)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }
    }
}