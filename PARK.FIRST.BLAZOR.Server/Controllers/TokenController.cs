using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARK.FIRST.BLAZOR.Server.Interface;
using PARK.FIRST.BLAZOR.Shared;

namespace PARK.FIRST.BLAZOR.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;

        public TokenController(IJwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] TokenViewModel vm)
        {
            var token = _tokenService.BuildToken(vm.Email);

            return Ok(new { token });
        }
    }
}