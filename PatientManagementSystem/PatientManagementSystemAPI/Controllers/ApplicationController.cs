using Microsoft.AspNetCore.Mvc;

namespace PatientManagementSystemAPI.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
