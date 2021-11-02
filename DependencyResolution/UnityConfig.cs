using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services;
using DataAccess;
using Microsoft.Practices.Unity;
using NepFlex.Core.Interfaces.Services;
using NepFlex.Core.Services;
using NepFlex.DataAccess.Context;

namespace DependencyResolution
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IOnlinePasalContext, OnlinePasalContext>(new TransientLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IMenuTopService, MenuTopService>();
            container.RegisterType<ISearchService, SearchService>();
            container.RegisterType<IItemDescriptionService, ItemDescriptionService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<IDetailService, DetailService>();
            container.RegisterType<ISendEmailService, SendEmailSevice>();
            container.RegisterType<ILoginService, LoginService>();
        }
    }
}
