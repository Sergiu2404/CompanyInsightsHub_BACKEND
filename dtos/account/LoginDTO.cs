using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace backend.dtos.account
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}