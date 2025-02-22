﻿using System.ComponentModel.DataAnnotations;

namespace ApiProject.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Username {  get; set; }

        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
