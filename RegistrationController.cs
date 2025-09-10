using PepsArts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PepsArts.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        //View Registration

        [HttpGet]
        public ActionResult RegisterExhibition()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RegisterExhibition(Registration reg)
        {
            try
            {
                var response = await APIHelper.PostJsonAsync("https://api.restful-api.dev/objects", reg);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Succefully added Registered for Exhibition";
                    return RedirectToAction("ViewExhibitions");
                }
                else
                {
                    return View(reg);
                }


            }
            catch (Exception e)
            {
                ViewBag.Error = "Error Failed to create Registration";
            }
            return View(reg);
        }

        public ActionResult ViewRegistrations()
        {
            return View();
        }









        public async Task<ActionResult> ViewRegistration(int id)
        {
            return View();
        }
        //to create a registration
        public async Task<ActionResult> AddRegistration()
        {
            return View();
        }
    }
}