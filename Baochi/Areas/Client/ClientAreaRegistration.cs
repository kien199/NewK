using System.Web.Mvc;

namespace Baochi.Areas.Client
{
    public class ClientAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Client";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Client_Category",
                "Client/{controller}/{slug}",
                new { controller = "Category", action = "Index", slug = UrlParameter.Optional }
            );

            context.MapRoute(
                "Client_Post",
                "Client/{controller}/{cateslug}/{postslug}",
                new { controller = "Post", action = "Index", cateslug = UrlParameter.Optional, postslug = UrlParameter.Optional }
            );

            context.MapRoute(
                "Client_default",
                "Client/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}