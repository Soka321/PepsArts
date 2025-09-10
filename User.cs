using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PepsArts.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide your Name")] //handling errors when no input
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide your Lastname")] //handling errors when no input
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please provide your Gender")] //handling errors when no input
        public string Gender { get; set; }
        public int Age { get; set; }
        [Required(ErrorMessage = "Please provide your email")] //handling errors when no input
        [EmailAddress(ErrorMessage = "Please provide valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please provide your Password")] //handling errors when no input
        [StringLength(100,MinimumLength=8,ErrorMessage = "Please provide your Password with minimum 8 characters")] //handling errors when no input
        [DataType(DataType.Password)]  
       public string Password { get; set; }
        [Required(ErrorMessage = "Please provide your phone number")] //handling errors when no input
        public int PhoneNumber { get; set; }
        
        public string Role { get; set; }
        public DateTime DateCreated { get; set; } 
      

        //public string Image { get; set; }
    }
}