using System.Web.Mvc;

namespace AddressBook.Areas.Web1
{
    public class Web1AreaRegistration : AreaRegistration
    {
        public override string AreaName => "Web1";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Web1_default",
                "Web1/{controller}/{action}/{id}",
                new { controller = "Web1", action = "Index", id = UrlParameter.Optional },
                new[] { "AddressBook.Areas.Web1.Controllers" }
            );
        }
    }
}