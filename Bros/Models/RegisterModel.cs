﻿using Bros.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class RegisterModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name="FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name="Gender")]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name="Zipcode")]
        public int Zipcode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}