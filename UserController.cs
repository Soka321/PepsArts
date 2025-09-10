using Newtonsoft.Json;
using PepsArts.Models;
using PepsArts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PepsArts.Controllers
{
    public class UserController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
       
            [HttpGet]
            public ActionResult Register()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Register(User user)
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                user.DateCreated = DateTime.Now;
                user.Role = "Visitor"; // or whatever default role you want

                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("https://localhost:2025/PepsArts/User/", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Registration successful!";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Error = "Registration failed. Please try again.";
                        return View(user);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error: " + ex.Message;
                    return View(user);
                }
            }
        

        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var response = await client.GetAsync("https://dummyjson.com/users");
                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<UserVM>(json);
                    return View(users.users);
                }
            }catch(Exception e)
            {
                ViewBag.Error = "Error failed to get users";
                return View(new List<User>());
            }
            ViewBag.Error = "Error failed to get users 2";
            return View(new List<User>());
        }
    }
}