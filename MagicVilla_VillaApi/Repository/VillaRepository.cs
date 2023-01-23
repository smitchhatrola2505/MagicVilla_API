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
	public class VillaRepository : Repository<Villa>,IVillaRepository 
	{
		private readonly ApplicationDbContext _db;
		public VillaRepository(ApplicationDbContext db) : base(db) 
		{
			_db = db;
		}

		
		public async Task<Villa> UpdateAsync(Villa entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_db.Villas.Update(entity);
			await _db.SaveChangesAsync();
			return entity;

		}
		

		
	}
}
