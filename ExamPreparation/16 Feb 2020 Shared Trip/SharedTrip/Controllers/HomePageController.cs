using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip.Controllers
{
    public class HomePageController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Trips/All");
            }
            
            return this.View();
        }
    }
}
