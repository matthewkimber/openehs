using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using OpenEhs.Data;

namespace OpenEhs.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// MvcApplication contains the registration of routes so that you can access pages like /Patient/1000
    /// and /Roles/ instead of the long format
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "UserAdminRoute",
                "Admin/User/{action}/{id}",
                new { controller = "User", action="Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "PagedRoute",
                "{controller}/{action}/Page{page}",
                null,
                new {page = @"\d+"}
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

#if DEBUG
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif
        }

        protected void Application_BeginRequest()
        {
            if (!UnitOfWork.IsStarted)
                UnitOfWork.Start();

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            // Localization Code - Forcing default to en-GB and changing the necessary items 
            //                     (i.e. currency symbol, etc.).
            var culture = new CultureInfo("en-GB", false);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencySymbol = "GH₵";
            Thread.CurrentThread.CurrentUICulture.DateTimeFormat.DateSeparator = "-";
            ////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        protected void Application_EndRequest()
        {
            UnitOfWork.CurrentSession.Flush();
            UnitOfWork.CurrentSession.Clear();
        }
    }
}