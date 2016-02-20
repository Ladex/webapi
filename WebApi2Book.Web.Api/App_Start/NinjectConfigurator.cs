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
using WebApi2Book.Common.Security;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessor;
using WebApi2Book.Web.Api.AutoMappingConfiguration;
using WebApi2Book.Web.Api.MaintainanceProcessing;
using WebApi2Book.Web.Common;
using WebApiBook.Data.SqlServer.Mapping;
using WebApiBook.Data.SqlServer.QueryProcessor;

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
            ConfigureAutoMapper(container);
            ConfigureLog4Net(container);
            ConfigureNHibernate(container);
            ConfigureUserSession(container);
            container.Bind<IDateTime>().To<DateTImeAdapter>().InSingletonScope();
            container.Bind<IAddTaskQueryProcessor>().To<AddTaskQueryProcessor>().InRequestScope();
            container.Bind<IAddTaskMaintenanceProcessing>().To<AddTaskMaintenanceProcessing>().InRequestScope();
        }

        private static void ConfigureAutoMapper(IKernel container)
        {
            container.Bind<IAutoMapper>().To<AutoMapperAdapter>().InSingletonScope();

            container.Bind<IAutoMapperTypeConfigurator>()
                .To<StatusEntityToStatusAutoMapperTypeConfigurator>()
                .InSingletonScope();

            container.Bind<IAutoMapperTypeConfigurator>()
                .To<StatusToStatueEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();

            container.Bind<IAutoMapperTypeConfigurator>()
                .To<UserEntityToUseAutoMapperTypeConfigurator>()
                .InSingletonScope();

            container.Bind<IAutoMapperTypeConfigurator>()
                .To<UserToUserEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();

            container.Bind<IAutoMapperTypeConfigurator>()
                .To<NewTaskToTaskEntityAutoMappingTypeConfiguration>()
                .InSingletonScope();

            container.Bind<IAutoMapperTypeConfigurator>()
                .To<TaskEntityToTaskAutoMapperTypeConfigurator>()
                .InSingletonScope();
        }

        private static void ConfigureUserSession(IKernel container)
        {
            var userSession = new UserSession();
            container.Bind<IUserSession>().ToConstant(userSession);
            container.Bind<IWebUserSession>().ToConstant(userSession);
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
                    MsSqlConfiguration.MsSql2005.ConnectionString(c => c.FromConnectionStringWithKey("WebApi2BookDb")))
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