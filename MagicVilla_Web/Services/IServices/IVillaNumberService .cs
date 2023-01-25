﻿using MagicVilla_Web.Models.Dto;
using System.Linq.Expressions;

namespace MagicVilla_Web.Services.IServices
{
	public interface IVillaNumberService
	{
		Task<T> GetAllAsync<T>();
		Task<T> GetAsync<T>(int id);

		Task<T> CreateAsync<T>(VillaNumberCreatedDTO dto);
		Task<T> UpdateAsync<T>(VillaNumberUpdatedDTO dto);
		Task<T> DeleteAsync<T>(int id);
	}
}
