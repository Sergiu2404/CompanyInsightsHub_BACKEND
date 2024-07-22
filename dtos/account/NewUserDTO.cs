using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.dtos.account
{
    public class NewUserDTO
    {
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
    }
}