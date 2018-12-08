using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public TokenController(IJwtTokenService tokenService, UserManager<IdentityUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        //[HttpPost]
        //public IActionResult GenerateToken([FromBody] TokenViewModel vm)
        //{
        //    var token = _tokenService.BuildToken(vm.Email);

        //    return Ok(new { token });
        //}

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] TokenViewModel vm)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var result = await _userManager.CreateAsync(new IdentityUser()
            {
                UserName = vm.Email,
                Email = vm.Email
            }, vm.Password);

            if (result.Succeeded == false)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] TokenViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);
            var correctUser = await _userManager.CheckPasswordAsync(user, vm.Password);

            if (correctUser == false)
            {
                return BadRequest("UserName or Password is incorrect !");
            }

            return Ok(new { token = GenerateToken(vm.Email) });
        }

        //[HttpPost]
        public string GenerateToken(string email)
        {
            var token = _tokenService.BuildToken(email);

            return token;
        }
    }
}