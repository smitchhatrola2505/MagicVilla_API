using MagicVilla_VillaApi.Models.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
	[Route("api/UserAuth")]
	[ApiController]
	public class UsersController : Controller
	{
		private readonly IUserRepository _userRepo;
		public UsersController(IUserRepository userRepo)
		{
			_userRepo= userRepo;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
		{
			var LoginReponse = await _userRepo.Login(model); 
			return View();
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
		{
			return View();
		}
	}
}
