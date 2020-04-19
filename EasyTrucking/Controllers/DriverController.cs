using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EasyTrucking.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class DriverController : Controller
    {
        private IConfiguration _configuration;
        private LoadsHandler handler;

        public DriverController(IConfiguration configuration)
        {
            _configuration = configuration;
            handler = new LoadsHandler(_configuration);
        }

        public IActionResult Index()
        {
            if (!ValidateLogin())
                return RedirectToAction("Index", "Login");
            
            var loads = handler.GetAvailableLoads();
            return View(loads);
        }

        [HttpPost]
        public IActionResult AcceptLoad(IFormCollection collection)
        {
            if (!ValidateLogin())
                return RedirectToAction("Index", "Login");

            handler.AcceptLoad(int.Parse(collection["id"]), int.Parse(HttpContext.Session.GetString("user_id")));
            TempData["success"] = "You have successfully accepted load.";
            return RedirectToAction("Index");
        }


        public IActionResult MyLoads()
        {
            if (!ValidateLogin())
                return RedirectToAction("Index", "Login");

            var loads = handler.GetMyLoads(int.Parse(HttpContext.Session.GetString("user_id")));
            return View(loads);
        }

        public IActionResult Payouts()
        {
            if (!ValidateLogin())
                return RedirectToAction("Index", "Login");

            var loads = handler.GetMyLoads(int.Parse(HttpContext.Session.GetString("user_id")));
            return View(loads);
        }


        public bool ValidateLogin()
        {
            if (HttpContext.Session.GetString("user_id") == null)
                return false;

            if (!HttpContext.Session.GetString("user_role").Equals("driver"))
                return false;

            return true;
        }
    }
}