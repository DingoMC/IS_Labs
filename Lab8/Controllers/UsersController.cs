using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Lab8_JWT.Model;
using IS_Lab8_JWT.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace IS_Lab8_JWT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController
	{
		private IUserService userService;

		public UsersController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpPost("authenticate")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Authenticate(AuthenticationRequest request)
		{
			var response = userService.Authenticate(request);
			if (response == null) return new BadRequestObjectResult(new { message = "Username or password is incorrect" });
			return new OkObjectResult(response);
		}

		[HttpGet("getall")]
		[Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public IActionResult GetAllUsers()
		{
			return new OkObjectResult(userService.GetUsers());
		}

		[HttpGet("count")]
		[Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public IActionResult GetCount()
		{
			return new OkObjectResult(new { userCount = userService.GetUsers().Count() });
		}

		[HttpGet("prime")]
		[Authorize(Roles = "number", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public IActionResult GetNumber()
		{
			int[] primes = { 2, 3, 5, 7, 11, 13 };
			int idx = RandomNumberGenerator.GetInt32(6);
			return new OkObjectResult(new { number = primes[idx] });
		}
	}
}
