using Newtonsoft.Json;
using PepsArts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PepsArts.Controllers
{
    public class ArtPieceController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: ArtPiece
        public ActionResult Index()
        {
            return View();
        }
         //to view art pieces and single artpiece
        public ActionResult ViewArtPieces()
        {
            List<ArtPiece> pieces = new List<ArtPiece>();

            return View(pieces);
        }


        [HttpGet, ActionName("ViewArtPieces")]
        public async Task<ActionResult> GetArtPieces()
        {
            try
            {
                var response = await client.GetAsync("https://dummyjson.com/users");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var pieces = JsonConvert.DeserializeObject<List<ArtPiece>>(json);
                    return View(pieces);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error failed to get Art Pieces";
                return View(new List<ArtPiece>());
            }
            ViewBag.Error = "Error failed to get Art Pieces";
            return View(new List<ArtPiece>());

            //return View();
        }
       
        //to create art piece
        [HttpGet]
        public ActionResult AddArtPiece()
        {
            return View(new ArtPiece());
        }

        [HttpPost]
        public async Task<ActionResult> AddArtPiece(ArtPiece piece,HttpPostedFileBase img)
        {
            if (!ModelState.IsValid)
            {
                return View(piece);
            }
            //adding artpiece image to API
            if (img != null && img.ContentLength > 0)
            {
                string filename = Path.GetFileName(img.FileName);
                string path = Path.Combine(Server.MapPath("/Uploads"), filename);
                img.SaveAs(path);
                piece.ImagePath = filename;
                var ex = new
                {
                    Title = piece.Title,
                    Description = piece.Description,
                    EstimatedValue = piece.EstimatedValue,
                    Status = piece.Status,
                    ImagePath= piece.ImagePath,
                   Artist_Id= piece.Artist_Id,
                   DateCreated = piece.DateCreated

                };

                var json = JsonConvert.SerializeObject(ex);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("https://localhost:2025/PepsArts/ArtPieces/", content);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Succefully created Art Piece";
                        return RedirectToAction("ViewArtPieces");
                    }
                    else
                    {
                        ViewBag.error = "Error could not add Exhition, please try";
                        return View(new ArtPiece());
                    }

                }
                catch (Exception e)
                {
                    ViewBag.Error = "Error Failed to create Art piece";
                    return View(piece);
                }
                
            }
            return View(piece);
            //  return View(piece);
        }
            
            
        //updating artpiece

       

        public ActionResult GetArtPiece()
        {
            return View(new ArtPiece());
        }
            //getting single artpiece
            [HttpGet]
        public async Task<ActionResult> GetArtPiece(int id)
        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/ArtPieces/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var artpiece = JsonConvert.DeserializeObject<ArtPiece>(json);

                    if (artpiece.artist == null)
                    {
                        artpiece.artist = new Artist(); // or fetch artist separately
                    }
                    return View(artpiece);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Could not load art piece";
            }
            ViewBag.Error = "Could not load art piece";
            return View(new ArtPiece());
        }
        //updating Artpiece

        [HttpGet]
        public async Task<ActionResult> UpdateArtPiece(int id)
        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/ArtPieces/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var artpiece = JsonConvert.DeserializeObject<ArtPiece>(json);
                    return View(artpiece);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Could not load Art Piece.";
            }
            ViewBag.Error = "Could not load Art Piece";
            return View(new ArtPiece());
        }


        [HttpPost]
        public async Task<ActionResult> UpdateArtPiece(ArtPiece piece)
        {

            var json = JsonConvert.SerializeObject(piece);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                // var response = await APIHelper.PostJsonAsync("https://api.restful-api.dev/objects", piece);
                var response = await client.PutAsync("https://localhost:2025/PepsArts/ArtPieces/"+ piece.Id,content);
                

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Succefully Updated Art piece";
                    return RedirectToAction("ViewArtPieces");
                }
                else
                {
                    ViewBag.error = "Error could not add Art Piece, please try";
                    return View();
                }


            }
            catch (Exception e)
            {
                ViewBag.Error = "Error Failed to create Artist";
            }
            return View(new ArtPiece());
        }
        //Deleting art piece
        [HttpGet]
        public async Task<ActionResult> DeleteArtPiece(int id)
        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/ArtPieces/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var artpiece = JsonConvert.DeserializeObject<ArtPiece>(json);
                    return View(artpiece);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Could not get Art Piece.";
                return View(new ArtPiece());
            }
            ViewBag.Error = "Could not get ArtPiece.";
            return View(new ArtPiece());

        }

        //getting that single Exhibition
        [HttpGet]
        public async Task<ActionResult> EditArtStatus(int id)
        {
            try
            {
                var response = await client.GetAsync("https://localhost:2025/PepsArts/ArtPiece/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var exhibition = JsonConvert.DeserializeObject<ArtPiece>(json);
                    return View(exhibition);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Could not load art piece.";
            }
            ViewBag.Error = "Could not load art piece.";
            return View(new ArtPiece());
        }


        //updating the Exhibition Status
        [HttpPost]
        public async Task<ActionResult> EditArtStatus(ArtPiece artpiece)
        {

            try
            {
                //serializing the data of object exhibition
                var json = JsonConvert.SerializeObject(artpiece);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://localhost:2025/PepsArts/ArtPieces/status/" + artpiece.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Successfully updated ArtWork status";
                    return RedirectToAction("GetExhibition");
                }
                return RedirectToAction("EditArtStatus");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error trying to update status, please try again";
                return View(new ArtPiece());
            }

            //return RedirectToAction("UpdateStatus");
        }


        [HttpPost]
       
            public async Task<ActionResult> DeleteArtPiece(ArtPiece artpiece)
            {

                try
                {
                    //serializing the data of object exhibition
                    var json = JsonConvert.SerializeObject(artpiece);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.DeleteAsync("https://localhost:2025/PepsArts/Exhibitions/" + artpiece.Id);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Successfully Deleted ArPiece" + "" + artpiece.Title + "";
                        return View(artpiece);
                    }
                    return RedirectToAction("ViewArtPieces");

                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error trying to delete art piece, please try again" + ex.Message;
                    return View(new ArtPiece());
                }

                //  return View(new Artist());
            }
        }
    }
