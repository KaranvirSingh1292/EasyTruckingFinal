using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.BusinessLogic;
using CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace EasyTrucking.Controllers
{
    public class LoginController : Controller
    {

        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                LoginHandler handler = new LoginHandler(_configuration);
                List<string> res = handler.VerifyUser(login);

                if(res[0]=="1")
                {
                    HttpContext.Session.SetString("user_id", res[1]);
                    HttpContext.Session.SetString("user_role", res[2]);

                    if (res[2]=="driver")
                    {
                        return RedirectToAction("Index", "Driver");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dispatcher");
                    }
                }

                else if(res[0]=="2")
                {
                    TempData["error"] = "Incorrect Password. Please try again";
                    return RedirectToAction("Index");
                }

                TempData["error"] = "Account doesn't exist. Please try again";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("user_id") == null)
                return RedirectToAction("Index");
            
            HttpContext.Session.Remove("user_id");
            HttpContext.Session.Remove("user_role");
            TempData["success"] = "You have been successfully logged out.";
            return RedirectToAction("Index");
        }
    }
}