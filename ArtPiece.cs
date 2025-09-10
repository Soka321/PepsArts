using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PepsArts.Models
{
    public class ArtPiece
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide name / Title for Art Piece")] //handling errors when no input
        public string Title { get; set; }
        [Required(ErrorMessage = "Please provide the description for Art Piece")] //handling errors when no input
        public string Description { get; set; }
        [Required(ErrorMessage = "Please provide the Esimated value for your Art Piece")] //handling errors when no input
        public Decimal EstimatedValue { get; set; }
        
        public int Artist_Id { get; set; }
        [ForeignKey("Artist_Id")]
        public Artist artist { get; set; }
       
        public string Status { get; set; }
        [Required(ErrorMessage = "Please provide Image for your Art piece")] //handling errors when no input
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Please provide date of creation for Art piece")] //handling errors when no input
        public DateTime DateCreated { get; set; }
       
    }
}