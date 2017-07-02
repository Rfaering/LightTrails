using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace PublicSite.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewData["Version"] = "0.3.2";
            return View();
        }

        [Route("/Usage")]
        public IActionResult Usage(string video)
        {
            string physicalWebRootPath = _hostingEnvironment.WebRootPath;
            if (!System.IO.File.Exists(Path.Combine(physicalWebRootPath, "video/" + video)))
            {
                return Redirect("/");
            }

            ViewData["Message"] = "How to use video online";
            ViewData["Video"] = video;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
