using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;

namespace backend.services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}