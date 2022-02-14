﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.DTO
{
    public class CreateUserRequest
    {
        [BindProperty(Name = "name")]
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }

        [BindProperty(Name = "email")]
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }

        [BindProperty(Name = "address")]
        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }

        [BindProperty(Name = "phone")]
        [Required(ErrorMessage = "The phone is required")]
        public string Phone { get; set; }

        [BindProperty(Name = "userType")]
        public string UserType { get; set; }

        [BindProperty(Name = "money")]
        public decimal Money { get; set; }
    }
}
