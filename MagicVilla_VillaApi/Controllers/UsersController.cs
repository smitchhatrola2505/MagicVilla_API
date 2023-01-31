using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{
	[Route("api/v{version:apiVersion}/UserAuth")]
	[ApiController]
	[ApiVersion("1.0")]

	public class UsersController : Controller
	{
		private readonly IUserRepository _userRepo;
		protected APIResponse _response;
		public UsersController(IUserRepository userRepo)
		{
			_userRepo= userRepo;
			this._response= new();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
		{
			var loginReponse = await _userRepo.Login(model); 
			if(loginReponse.User == null || string.IsNullOrEmpty(loginReponse.Token))
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess= false;
				_response.ErrorMessage.Add("Username or Password Is Incorrect");
				return BadRequest(_response);
			}
			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			_response.Result= loginReponse;
			return Ok(_response);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
		{
			bool ifUserNameUnique = _userRepo.IsUniqueUser(model.UserName);

			if (!ifUserNameUnique	)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessage.Add("Username Already Exists");
				return BadRequest(_response);
			}

			var user = await _userRepo.Register(model);
			if(user == null)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessage.Add("Error While Registrating");
				return BadRequest(_response);
			}

			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;

			return Ok(_response);
		}
	}
}
