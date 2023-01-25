using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
	public class VillaNumberServices : BaseServices, IVillaNumberService
	{
		private readonly IHttpClientFactory _ClientFactory;
		private string villaUrl;

		public VillaNumberServices(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_ClientFactory = clientFactory;
			villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
		}

		public Task<T> CreateAsync<T>(VillaNumberCreatedDTO dto)
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
				Url = villaUrl + "/api/villaNumberAPI/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/villaNumberAPI"
			});
		}

		public Task<T> GetAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/villaNumberAPI/" + id
			});
		}

		public Task<T> UpdateAsync<T>(VillaNumberUpdatedDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = villaUrl + "/api/villaNumberAPI/" +dto.VillaNo
			});
		}
	}
}
