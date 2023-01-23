﻿using AutoMapper;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository
{
	public class VillaNumberRepository : Repository<VillaNumber>,IVillaNumberRepository
	{
		private readonly ApplicationDbContext _db;
		public VillaNumberRepository(ApplicationDbContext db) : base(db) 
		{
			_db = db;
		}

		
		public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_db.VillaNumbers.Update(entity);
			await _db.SaveChangesAsync();
			return entity;

		}
		

		
	}
}
