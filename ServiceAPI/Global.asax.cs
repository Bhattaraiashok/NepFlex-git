﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Newtonsoft.Json;


namespace ServiceAPI
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


        }
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
        }
    }
}