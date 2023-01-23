using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Models.Dto
{
	public class VillaNumberCreatedDTO
	{

		[Required] 
		public int VillaNo { get; set; }
		public string SpecialDetails { get; set; }
	}
}
