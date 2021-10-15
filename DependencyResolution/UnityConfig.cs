using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Core.Interfaces.Services;
using Core.Services;
using Core.Interfaces.Repositories;
using DataAccess.Repositories;
using NepFlex.DataAccess.Context;
using Core.Interfaces;
using DataAccess;
using NepFlex.Core.Interfaces.Services;
using NepFlex.Core.Services;
using Microsoft.AspNet.Identity;
using NepFlex.Core.Entities.ResourceModels;
using Microsoft.AspNet.Identity.EntityFramework;

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
