﻿using System.ComponentModel.DataAnnotations;

namespace POSY.Infra.CrossCutting.Identity.Model
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar login?")]
        public bool RememberMe { get; set; }
    }
}