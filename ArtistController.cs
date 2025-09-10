using Newtonsoft.Json;
using PepsArts.Models;
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
    public class ArtistController : Controller
    {
        //initializing object
        HttpClient client= new HttpClient();
        /*public ArtistController(HttpClient c)
        {
            client = c;
        }*/
        // GET: Artist
       /* public ActionResult ArtistProfile()
        {
            return View(new List<Artist>());
        }*/
        
        [HttpGet]
        public async Task<ActionResult> ArtistProfile()
        {
            try
            {
                //get all artist profiles in a list then give them to the view
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Artists/");
                if (response.IsSuccessStatusCode)
                {
                    //convert list from API
                    var json = await response.Content.ReadAsStringAsync();
                    var artists = JsonConvert.DeserializeObject<List<Artist>>(json); //new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ;
                    return View(artists);
                }
            }catch(Exception ex)
            {
                ViewBag.Error = "Could not get Artists";
                return View(new List<Artist>());
            }
            return View(new List<Artist>());
        }

        [HttpGet]
        public  ActionResult CreateArtist()
        {
            return View(new Artist());
        }
        [HttpPost]
        public async Task<ActionResult> CreateArtist(Artist artist)
        {
            try
            {
                var response = await APIHelper.PostJsonAsync("https://api.restful-api.dev/objects", artist);

                if(response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Succefully added Artist profile";
                    return RedirectToAction("ArtistProfile");
                }
                else
                {
                    return View(artist);
                }

                
            }catch(Exception e)
            {
                ViewBag.Error = "Error Failed to create Artist";
            }
            return View(artist);
        }

        //getting that single Exhibition
        [HttpGet]
        public async Task<ActionResult> UpdateArtist(int id)
        {
            var response = await client.GetAsync("https://localhost:2025/PepsArts/Artists/" + id);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var artist = JsonConvert.DeserializeObject<Artist>(json);
                return View(artist);
            }
            ViewBag.Error = "Could not get artist.";
            return View(new Artist());
        }


        public ActionResult GetArtist()
        {
            return View(new Artist());
        }
        //getting single artpiece
        [HttpGet]
        public async Task<ActionResult> GetArtist(int id)
        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Artists/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var artpiece = JsonConvert.DeserializeObject<Artist>(json);

                   /* if (artpiece. == null)
                    {
                        artpiece.artist = new Artist(); // or fetch artist separately
                    }  */
                    return View(artpiece);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Could not load artist";
            }
            ViewBag.Error = "Could not load artist";
            return View(new Artist());
        }

        //updating the Exhibition Status
        [HttpPost]
        public async Task<ActionResult> UpdateArtist(Artist artist)
        {

            try
            {
                //serializing the data of object exhibition
                var json = JsonConvert.SerializeObject(artist);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:2025/PepsArts/Artist/" + artist.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Successfully updated Artist"+ ""+ artist.Name + ""+ artist.Surname;
                    return RedirectToAction("ArtistProfile");
                }
                return RedirectToAction("UpdateArtist");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error trying to update artist, please try again";
                return View(new Artist());
            }

          //  return View(new Artist());
        }

        [HttpGet]
        public async Task<ActionResult> DeleteArtist(int Id)

        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/Artists/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var artist = JsonConvert.DeserializeObject<Artist>(json);
                    return View(artist);
                }
            }catch(Exception ex)
            {
                ViewBag.Error = "Could not get artist.";
                return View(new Artist());
            }
            ViewBag.Error = "Could not get artist.";
            return View(new Artist());
            
        }

        [HttpPost]
        public async Task<ActionResult> DeleteArtist(Artist artist)
        {

            try
            {
                //serializing the data of object exhibition
                var json = JsonConvert.SerializeObject(artist);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                
                var response = await client.DeleteAsync("https://localhost:2025/PepsArts/Artist/" + artist.Id);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Successfully Deleted Artist" + "" + artist.Name + "" + artist.Surname;
                    return View(artist);
                }
                return RedirectToAction("DeleteArtist");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error trying to update artist, please try again";
                return View(new Artist());
            }

            //  return View(new Artist());
        }

    }
}