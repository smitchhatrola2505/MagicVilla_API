using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
	public class VillaNumberUpdatedDTO
	{

		[Required] 
		public int VillaNo { get; set; }

		[Required]
		public int VillaID { get; set; }

		public string SpecialDetails { get; set; }
		public IEnumerable<SelectListItem> VillaList { get; internal set; }
	}
}
