using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
	public class VillaServices : BaseServices, IVillaService
	{
		private readonly IHttpClientFactory _ClientFactory;
		private string villaUrl;

		public VillaServices(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_ClientFactory = clientFactory;
			villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
		}

		public Task<T> CreateAsync<T>(VillaCreateDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = villaUrl + "/api/villaAPI"
			});
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = villaUrl + "/api/villaAPI/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/villaAPI"
			});
		}

		public Task<T> GetAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/villaAPI/" + id
			});
		}

		public Task<T> UpdateAsync<T>(VillaDTOUpdate dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = villaUrl + "/api/villaAPI/" +dto.Id
			});
		}
	}
}
