﻿using Sprydon.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace Sprydon.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<User>("Users");
            config.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: "odata",
                model: builder.GetEdmModel());
        }
    }
}
