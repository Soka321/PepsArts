using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PepsArts.Models
{
    public class ExhibitionArtPieces
    {
        public int Id { get; set; }
        public int Exhibition_Id { get; set; }
        [ForeignKey("Exhibition_Id")]
        public Exhibition exhibition { get; set; }
        public int ArtPiece_Id { get; set; }
        [ForeignKey("ArtPiece_Id")]
        public  ArtPiece Piece { get; set; }
    }
}