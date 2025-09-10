using Newtonsoft.Json;
using PepsArts.Models;
using PepsArts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PepsArts.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginVM log)
        {
            if (!ModelState.IsValid)
            {
                return View(log);
            }
            try
            {
                var response = await APIHelper.PostJsonAsync("https://api.restful-api.dev/objects", log);
                Session["UserId"] = 3;//User.Id;
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var User = JsonConvert.DeserializeObject<User>(json);
                    Session["Role"] = User.Role;
                     Session["Name"] = User.FirstName;
                    Session["UserId"] = User.Id;

                    ViewBag.Message = "Succefully Logged in"+ ""+ Session["Name"];
                    
                    return RedirectToAction("Home");
                }
                else
                {
                    ViewBag.error = "Error could not Login, please try";
                    return View(log);
                }


            }
            catch (Exception e)
            {
                ViewBag.Error = "Error Failed to create Artist";
            }
            return View(log);

        }

        public ActionResult LogOut()
        {
            return View(new LoginVM());
        }

       
        public ActionResult LogOut(string msg)
        {
            Session["Role"] = "";
            Session["Name"] = "";
            msg = "Successfully logged out";
            ViewBag.Messsage = msg;
            return RedirectToAction("/Home/Index");

        }
    }
}