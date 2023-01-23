using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Models.Dto
{
	public class VillaNumberUpdatedDTO
	{

		[Required] 
		public int VillaNo { get; set; }
		public string SpecialDetails { get; set; }
	}
}
