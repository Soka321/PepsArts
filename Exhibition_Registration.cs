using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PepsArts.Models
{
    public class Exhibition_Registration
    {
        public int Id { get; set; }
        public int Exhibition_Id { get; set; }
        [ForeignKey("Exhibition_Id")]
        public Exhibition exhibition { get; set; }
        public int User_Id { get; set; }//this is the visitor Id
        [ForeignKey("User_Id")]
        public User user { get; set; }

        public DateTime RegistrationDate { get; set; }
        public int NumberOfAttendees { get; set; }
    }
}