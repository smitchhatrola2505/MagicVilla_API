using Microsoft.AspNetCore.Identity;

namespace MagicVilla_VillaApi.Models
{
	public class ApplicatonUser : IdentityUser
	{
		public String Name { get; set; }
	}
}
