using MagicVilla_VillaApi.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,string? includeProperties = null,
			int pageSize=20,int pageNumber=1);
		Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
		Task CreateAsync(T entity);
		Task RemoveAsync(T villa);
		Task SaveAsync();
	}
}
