namespace UniStart.Controllers
{
    using System.Web.Http;

    [JsonConfiguration]
    public class PingController : ApiController
    {
        [HttpGet]
        public string Ping()
        {
            return "The service is currently running";
        }
    }
}
