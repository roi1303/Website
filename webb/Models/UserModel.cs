using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoiWeb.Models
{

    public class UserModel
    {
        //primry key
        public int Id { get; set; }


        [Display(Name = "UserName")] // Display is used to change the name of the field in the view
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }


        [Display(Name = "Email")] 
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(110, ErrorMessage = "The email must be at least 10 and at max 110 characters long.", MinimumLength = 9)]
        public string Email { get; set; }



        [Display(Name = "Password")] // Display is used to change the name of the field in the view
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}