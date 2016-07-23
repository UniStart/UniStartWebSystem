namespace UniStart.Controllers
{
    using System.Web.Http;

    public class PingController : ApiController
    {
        [HttpGet]
        public string Ping()
        {
            return "The service is currently running";
        }
    }
}