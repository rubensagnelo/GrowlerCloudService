﻿using System.Web;
using System.Web.Mvc;

namespace web.api.growler.Sensor
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
