using System.Web.Mvc;

namespace AddressBook.Areas.Web2
{
    public class Web2AreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Web2";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Web2_default",
                "Web2/{controller}/{action}/{id}",
                new { controller = "Web2", action = "Index", id = UrlParameter.Optional },
                new[] { "AddressBook.Areas.Web2.Controllers" }
            );
        }
    }
}