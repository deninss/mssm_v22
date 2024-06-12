using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mssm.Context;
using mssm.Models.TeamModel;
using mssm.Services.ITeam;
using mssm.Services.ISearch;
using Newtonsoft.Json;
using System.Security.Claims; 

namespace mssm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult project()
        {
            return RedirectToAction("Index", "Project");
        }
        public IActionResult calendar()
        {
            return RedirectToAction("Index", "Calendar");
        }
        public IActionResult task()
        {
            return RedirectToAction("Index", "Task");
        }
      
        public IActionResult team()
        {
            return RedirectToAction("Index", "Team");
        }

    }
}
