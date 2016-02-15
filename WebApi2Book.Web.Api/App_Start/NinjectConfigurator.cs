using log4net.Config;
using Ninject;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using Ninject.Activation;
using Ninject.Web.Common;
using WebApi2Book.Web.Common;
using WebApiBook.Data.SqlServer.Mapping;

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
            ConfigureNHibernate(container);
            container.Bind<IDateTime>().To<DateTImeAdapter>().InSingletonScope();
        }

        private static void ConfigureLog4Net(IKernel container)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }

        private static void ConfigureNHibernate(IKernel container)
        {
            var sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("WebApi2BookDb")))
                .CurrentSessionContext("web")
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                .BuildSessionFactory();

            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            container.Bind<ISession>().ToMethod(CreateSession).InRequestScope();
            container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>().InRequestScope();
        }

        private static ISession CreateSession(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);

                return sessionFactory.GetCurrentSession();
            }
            return sessionFactory.GetCurrentSession();
        }
    }
}