using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PARK.FIRST.BLAZOR.Server.Interface
{
    public interface IJwtTokenService
    {
        string BuildToken(string email);
    }
}
