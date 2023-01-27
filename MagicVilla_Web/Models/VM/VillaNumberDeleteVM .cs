using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.VM
{
	public class VillaNumbersDeleteVM
	{
		public VillaNumbersDeleteVM()
		{
			VillaNumber = new VillaNumberDTO();
		}
		public VillaNumberDTO VillaNumber { get; set; }
		
		[ValidateNever]
		public IEnumerable<SelectListItem> VillaList { get; set; }
	}
}
