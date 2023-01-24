using AutoMapper;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web
{
	public class MappingConfig : Profile
	{


		public MappingConfig()
		{
			CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap(); 
			CreateMap<VillaDTO, VillaDTOUpdate>().ReverseMap(); 

			CreateMap<VillaNumberDTO, VillaNumberCreatedDTO>().ReverseMap();
			CreateMap<VillaNumberDTO, VillaNumberUpdatedDTO>().ReverseMap();

			
		}
	}
}
