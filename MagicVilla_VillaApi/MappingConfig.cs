using AutoMapper;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;

namespace MagicVilla_VillaApi
{
	public class MappingConfig : Profile
	{


		public MappingConfig()
		{
			CreateMap<Villa,VillaDTO>(); 
			CreateMap<VillaDTO, Villa>(); 

			CreateMap<Villa, VillaCreateDTO>().ReverseMap();
			CreateMap<Villa, VillaDTOUpdate>().ReverseMap();
			
			CreateMap<VillaNumber,VillaNumberDTO>().ReverseMap(); 

			CreateMap<VillaNumber, VillaNumberCreatedDTO>().ReverseMap();
			CreateMap<VillaNumber, VillaNumberUpdatedDTO>().ReverseMap();
			CreateMap<ApplicatonUser,UserDTO>().ReverseMap();
			
		}
	}
}
