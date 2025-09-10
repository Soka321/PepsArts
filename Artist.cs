using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PepsArts.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide name for Artist")] //handling errors when no input
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide Surname for Artist")] //handling errors when no input
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please provide Biography for Artist")] //handling errors when no input
        public string Biography { get; set; }
        [Required(ErrorMessage = "Please provide email for Artist")] //handling errors when no input
        [EmailAddress(ErrorMessage ="Please provide valid e-mail address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please provide phone number for Artist")] //handling errors when no input
        public int PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please provide country for Artist")] //handling errors when no input
        public string Country { get; set; }
        [Required(ErrorMessage = "Please provide City for Artist")] //handling errors when no input
        public string City { get; set; }
        [Required(ErrorMessage = "Please provide date of creation for Artist")] //handling errors when no input
        public DateTime DateCreated { get; set; }
    }
}