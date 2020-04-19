using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.BusinessLogic;
using CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EasyTrucking.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class DispatcherController : Controller
    {
        private IConfiguration _configuration;
        private LoadsHandler handler;

        public DispatcherController(IConfiguration configuration)
        {
            _configuration = configuration;
            handler = new LoadsHandler(_configuration);
        }


        public IActionResult Index()
        {
            if (!ValidateLogin())
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public IActionResult Create(LoadsModel model)
        {
            model.DispatcherID = int.Parse(HttpContext.Session.GetString("user_id"));
            handler.CreateLoad(model);
            TempData["success"] = "Your loads has been added successfully.";
            return RedirectToAction("Index");
        }

        public IActionResult MyLoads()
        {
            if (!ValidateLogin())
                return RedirectToAction("Index", "Login");

            var loads = handler.GetMyLoadsDP(int.Parse(HttpContext.Session.GetString("user_id")));
            return View(loads);
        }

        public IActionResult AcceptedLoads()
        {
            if (!ValidateLogin())
                return RedirectToAction("Index", "Login");

            var loads = handler.GetAcceptedLoadsDP(int.Parse(HttpContext.Session.GetString("user_id")));
            return View(loads);
        }

        public bool ValidateLogin()
        {
            if (HttpContext.Session.GetString("user_id") == null)
                return false;

            if (!HttpContext.Session.GetString("user_role").Equals("dispatcher"))
                return false;

            return true;
        }
    }
}