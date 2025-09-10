using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PepsArts.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Please provide name for Exhibition")] //handling errors when no input
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide Description for Exhibition")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please provide Start Date for Exhibition")]

        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please provide End Date for Exhibition")]
        public DateTime EndDate { get; set; }
        
        public string Status { get; set; }
        public int User_Id { get; set; }
        //Created by which owner
        [ForeignKey("User_Id")]
        public User user { get; set; }
    }
}