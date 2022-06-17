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


        [Display(Name = "UserName")] //display name in the form =->to Username
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }


        [Display(Name = "Email")] //display name in the form =->to Email
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(110, ErrorMessage = "The email must be at least 10 and at max 110 characters long.", MinimumLength = 9)]
        public string Email { get; set; }



        [Display(Name = "Password")] //display name in the form =->to Password
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}