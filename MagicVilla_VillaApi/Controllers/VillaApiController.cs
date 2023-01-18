using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
	[Route("api/VillaApi")]
	[ApiController]
	public class VillaApiController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<VillaDTO> GetVillas()
		{
			return new List<VillaDTO>()
			{
				new VillaDTO{Id=1,Name="Pool View"},
				new VillaDTO{Id=2,Name="Beach View"} 
			};
		}
	}
}
