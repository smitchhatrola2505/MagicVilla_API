using MagicVilla_Web.Models.Dto;
using System.Linq.Expressions;

namespace MagicVilla_Web.Services.IServices
{
	public interface IVillaService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetAsync<T>(int id);

		Task<T> CreateAsync<T>(VillaCreateDTO dto);
		Task<T> UpdateAsync<T>(VillaDTOUpdate dto);
		Task<T> DeleteAsync<T>(int id);
	}
}
