using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PepsArts.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using PepsArts.ViewModels;
using System.Net;

namespace PepsArts.Controllers
{
   
    public class ExhibitionController : Controller
    {

        private HttpClient client = new HttpClient();
        // GET: Exhibition
        public ActionResult Index()
        {
            return View();
        }

        public  ActionResult ViewExhibitions()
        {
            return View(new List<Exhibition>());
        }

        //getting list of exhibitions
        [HttpGet]
        public async Task<ActionResult> GetExhibitions()
        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Exhibition");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var exhibitions = JsonConvert.DeserializeObject<List<Exhibition>>(json);
                    return View(exhibitions);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error failed to get exhibitions";
                return View(new List<Exhibition>());
            }
            ViewBag.Error = "Error failed to get Exhibitions";
            return View(new List<Exhibition>());
        }


        //getting a list of registrations

        public ActionResult GetRegistrations()
        {
            return View(new List<Exhibition_Registration>());
        }

        //getting list of exhibitions
        [HttpGet, ActionName("GetRegistrations")]
        public async Task<ActionResult> GetExhibitionRegistrations()
        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Exhibition");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var exhibitions = JsonConvert.DeserializeObject<List<Exhibition_Registration>>(json);
                    return View(exhibitions);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error failed to get exhibitions";
                return View(new List<Exhibition_Registration>());
            }
            ViewBag.Error = "Error failed to get Exhibitions";
            return View(new List<Exhibition_Registration>());
        }


        [HttpGet]
        public ActionResult AddExhibition()
        {
           
            return View(new ExhibitionVM());

        }
        //creating an exhibition
        [HttpPost]
        public async Task<ActionResult> AddExhibition(ExhibitionVM exhibition)
        {
            if (!ModelState.IsValid)
            {
                return View(exhibition);
            }
            var ex = new  { Name = exhibition.Name,
            StartDate = exhibition.StartDate,
                EndDate = exhibition.EndDate,
                SelectedArtworksIds = exhibition.SelectedArtworksIds

            };

            var json = JsonConvert.SerializeObject(ex);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            try
            {
                var response = await client.PostAsync("https://localhost:2025/PepsArts/Exhibition/", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Succefully created Exhibition";
                    return RedirectToAction("ViewExhibitions");
                }
                else
                {
                    ViewBag.error = "Error could not add Exhition, please try";
                    return View(exhibition);
                }

                var result = await client.GetAsync("https://localhost:2025/PepsArts/Artworks/");
                var jsn = await result.Content.ReadAsStringAsync();
               var artworks = JsonConvert.SerializeObject(jsn);
               exhibition.Artworks = JsonConvert.DeserializeObject<List<ArtPiece>>(jsn);
                return View(exhibition);

            }
            catch (Exception e)
            {
                ViewBag.Error = "Error Failed to create Artist"+""+e.Message;
            }
            return View(exhibition);
           
        }

        /*
                [HttpGet]
                public ActionResult ExhibitionRegister(int exhibition_id)
                {
                    //getting exhibition id
                    ViewBag.ExhibitionId = exhibition_id;
                    ViewBag.UserId = Session["UserId"];
                    return View(new Exhibition_Registration());

                }
                //creating an exhibition
                [HttpPost]
                public async Task<ActionResult> ExhibitionRegister(int exhibition_Id, int numberOfPeople)
                {
                    int userId = (int)Session["UserId"];
                    if (!ModelState.IsValid)
                    {
                        return View(new Exhibition_Registration());
                    }
                    var ex = new
                    {
                        exhibition_Id,
                        userId,
                        numberOfPeople

                    };

                    //sending request to server

                    var json = JsonConvert.SerializeObject(ex);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    try
                    {
                        var response = await client.PostAsync("https://localhost:2025/PepsArts/Exhibition/", content);

                        if (response.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Succefully created Registered for Exhibition";
                            Session["Registered"] = "Registered";
                            return RedirectToAction("ViewExhibitions");
                        }
                        else
                        {
                            ViewBag.error = "Error could not register for Exhibition, please try";
                            return View(new Exhibition_Registration());
                        }


                        return View(new Exhibition_Registration());

                    }
                    catch (Exception e)
                    {
                        ViewBag.Error = "Error Failed to register for Exhibition" + "" + e.Message;
                    }
                    return View(new Exhibition_Registration());

                }
                public async Task<ActionResult> UpdateExhibition()
                {
                    return View();
                }
        */

        //Register for exhibition

        [HttpGet]
        public async Task<ActionResult> RegisterExhibition(int Exhibition_id)
        {
            /* if (Exhibition_id == null)
             {
                 ViewBag.Error = "Exhibition ID is missing.";
                 return View(new Exhibition_Registration());
             }

             try
             {
                 var response = await client.GetAsync("https://localhost:2025/PepsArts/Exhibitions/Register/" + Exhibition_id);
                 if (response.IsSuccessStatusCode)
                 {
                     var json = await response.Content.ReadAsStringAsync();
                     var register = JsonConvert.DeserializeObject<Exhibition_Registration>(json);
                     return View(register);
                 }
             }
             catch (Exception ex)
             {
                 ViewBag.Error = "Could not load Registration for exhibition.";
             }
             ViewBag.Error = "Could not load Registration for exhibition";
             return View(new Exhibition_Registration());

             */

            
                try
                {
                    var response = await client.GetAsync("https://localhost:2025/PepsArts/Exhibitions/Register/" + Exhibition_id);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var register = JsonConvert.DeserializeObject<Exhibition_Registration>(json);
                        register.Exhibition_Id = Exhibition_id; // Ensure it's set
                        register.User_Id = Convert.ToInt32(Session["UserId"]); // Set user from session
                        register.RegistrationDate = DateTime.Now;
                        return View(register);
                    }
                }
                catch
                {
                    ViewBag.Error = "Could not load Registration for exhibition.";
                }

                return View(new Exhibition_Registration());
            

        }


        [HttpPost]
        public async Task<ActionResult> RegisterExhibition(Exhibition_Registration reg)
        {
            /* //checking model state
             if(!ModelState.IsValid)
             {
                 return View(reg);
             }
             var json = JsonConvert.SerializeObject(reg);
             var content = new StringContent(json, Encoding.UTF8, "application/json");
             try
             {
                 // var response = await APIHelper.PostJsonAsync("https://api.restful-api.dev/objects", piece);
                 var response = await client.PostAsync("https://localhost:2025/PepsArts/Exhibitions/Register" + reg.Id, content);


                 if (response.IsSuccessStatusCode)
                 {
                    // ViewBag.Message = "Succefully Registered for Exhibition";

                     ViewBag.Message = "Succefully created Registered for Exhibition";
                     Session["Registered"] = "Registered";
                     return RedirectToAction("GetExhibition");
                     //return RedirectToAction("ViewArtPieces");
                 }
                 else
                 {
                     ViewBag.error = "Error could not Register for Exhibition, please try";
                     return View(reg);
                 }


             }
             catch (Exception e)
             {
                 ViewBag.Error = "Error Failed to Register for exhibition";
             }
             return View(new Exhibition_Registration());*/

           
                if (!ModelState.IsValid)
                {
                    return View(reg);
                }

                var json = JsonConvert.SerializeObject(reg);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("https://localhost:2025/PepsArts/Exhibitions/Register/" + reg.Exhibition_Id, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Session["Registered"] = "Registered";
                        ViewBag.Message = "Successfully registered for exhibition";
                        return RedirectToAction("GetExhibition");
                    }
                    else
                    {
                        ViewBag.Error = "Error: Could not register for exhibition.";
                        return View(reg);
                    }
                }
                catch
                {
                    ViewBag.Error = "Failed to register for exhibition.";
                    return View(reg);
                }
            

        }

        /* [HttpGet]
         public ActionResult ExhibitionRegister(int? exhibition_id)
         {
             if (!exhibition_id.HasValue)
             {
                 return RedirectToAction("ViewExhibitions"); // or show an error view
             }

             if (Session["UserId"] == null)
             {
                 return RedirectToAction("Login", "User"); // or wherever your login page is
             }

             var registration = new Exhibition_Registration
             {
                 Exhibition_Id = exhibition_id.Value,
                 User_Id = (int)Session["UserId"],
                 RegistrationDate = DateTime.Now
             };

             return View(registration);
         }

         [HttpPost]
         public async Task<ActionResult> ExhibitionRegister(Exhibition_Registration registration)
         {
             registration.User_Id = (int)Session["UserId"];
             registration.RegistrationDate = DateTime.Now;

             if (!ModelState.IsValid)
             {
                 return View(registration);
             }

             var payload = new
             {
                 exhibition_Id = registration.Exhibition_Id,
                 userId = registration.User_Id,
                 numberOfPeople = registration.NumberOfAttendees
             };

             var json = JsonConvert.SerializeObject(payload);
             var content = new StringContent(json, Encoding.UTF8, "application/json");

             try
             {
                 var response = await client.PostAsync("https://localhost:2025/PepsArts/Exhibition/", content);

                 if (response.IsSuccessStatusCode)
                 {
                     Session["Registered"] = "Registered";
                     TempData["SuccessMessage"] = "Successfully registered for exhibition.";
                     return RedirectToAction("ViewExhibitions");
                 }
                 else
                 {
                     ViewBag.Error = "Registration failed. Please try again.";
                     return View(registration);
                 }
             }
             catch (Exception ex)
             {
                 ViewBag.Error = "Error: " + ex.Message;
                 return View(registration);
             }
         }
        */

        //getting that single Exhibition
        [HttpGet]
        public async Task<ActionResult> EditStatus(int id)
        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Exhibition/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var exhibition = JsonConvert.DeserializeObject<Exhibition>(json);
                    return View(exhibition);
                }
            }catch(Exception ex)
            {
                ViewBag.Error = "Could not load exhibition.";
            }
            ViewBag.Error = "Could not load exhibition.";
            return View(new Exhibition());
        }


        //updating the Exhibition Status
        [HttpPost]
        public async Task<ActionResult> EditStatus(Exhibition exhibition)
        {
           
            try
            {
                //serializing the data of object exhibition
                var json =  JsonConvert.SerializeObject(exhibition);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:2025/PepsArts/Exhibition/status/"+ exhibition.Id, content);
                if(response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Successfully updated Exhibition status";
                    return RedirectToAction("ViewExhibitions");
                }
                return RedirectToAction("UpdateStatus");

            }
            catch(Exception ex)
            {
                ViewBag.Error = "Error trying to update status, please try again";
                return View(new Exhibition());
            }

            //return RedirectToAction("UpdateStatus");
        }

        //getting that single Exhibition
        [HttpGet]
        public async Task<ActionResult> DeleteExhibition(int id)
        {

            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Exhibitions/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var exhibition = JsonConvert.DeserializeObject<Exhibition>(json);
                    return View(exhibition);
                }
            }catch(Exception ex)
            {
                ViewBag.Error = "Could not get Exhibition.";
                return View(new Exhibition());
            }
             ViewBag.Error = "Could not get Exhibition.";
             return View(new Exhibition());   

            //testing code
           /* id = 5;
            var exhibition = new Exhibition
            {
                Id = id,
                Name = "My Exhibition"

            };
                return View(exhibition);*/
        }

        public ActionResult GetExhibition()
        {
            return View(new ExhibitionVM());
        }
        //Get Single Exhibition
        [HttpGet]
        public async Task<ActionResult> GetExhibition(int id)
        {

            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Exhibitions/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var exhibition = JsonConvert.DeserializeObject<ExhibitionVM>(json);
                    return View(exhibition);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Could not get Exhibition";
                return View(new ExhibitionVM());
            }
            ViewBag.Error = "Could not get Exhibition";
            return View(new ExhibitionVM());

            //testing code
            /* id = 5;
             var exhibition = new Exhibition
             {
                 Id = id,
                 Name = "My Exhibition"

             };
                 return View(exhibition);*/
        }

        [HttpGet]
        public async Task<ActionResult> EditExhibition(int? id)
        {
            if (id == null)
            {
                id = 1; // or any fallback value
            }

            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Exhibitions/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var exhibition = JsonConvert.DeserializeObject<ExhibitionVM>(json);
                    return View(exhibition);
                }
            }catch(Exception e)
            {
                ViewBag.Error = "Could not get Exhibition."+e.Message;
            }

            ViewBag.Error = "Could not get Exhibition.";
            return View(new ExhibitionVM());
        }



        [HttpPost]
        public async Task<ActionResult> EditExhibition(ExhibitionVM exhibition)
        {

            try
            {
                //serializing the data of object exhibition
                var json = JsonConvert.SerializeObject(exhibition);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:2025/PepsArts/Exhibition/" + exhibition.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Successfully updated Exhibition status";
                    return RedirectToAction("ViewExhibitions");
                }
                return RedirectToAction("UpdateStatus");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error trying to update status, please try again";
                return View(new ExhibitionVM());
            }

            //return RedirectToAction("UpdateStatus");
        }



        //updating the Exhibition Status
        [HttpPost]
        public async Task<ActionResult> DeleteExhibition(Exhibition exhibition)
        {

            try
            {
                //serializing the data of object exhibition
                var json = JsonConvert.SerializeObject(exhibition);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.DeleteAsync("https://localhost:2025/PepsArts/Exhibitions/" + exhibition.Id);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Successfully Deleted Exhibition" + "" + exhibition.Name + "" ;
                    return View(exhibition);
                }
                return RedirectToAction("DeleteExhibition");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error trying to delete exhibition, please try again"+ ex.Message;
                return View(new Exhibition());
            }

            //  return View(new Artist());
        }



    }
}