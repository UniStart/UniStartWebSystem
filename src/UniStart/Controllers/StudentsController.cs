using System.Web.Mvc;
using Unistart.Models;
using UniStart.Data;

namespace UniStart.Controllers
{
    public class StudentsController : Controller
    {
        private UniStartContext context;

        public StudentsController()
        {
            context = new UniStartContext();
        }
        // GET: Students
        public ActionResult Index()
        {
            context.Lectures.Add(new Lecture() {Title = "First Lecure"});
            return null;
        }
    }
}