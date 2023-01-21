using AutoMapper;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Controllers
{
	[Route("api/VillaApi")]
	[ApiController]
	public class VillaApiController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;
		public VillaApiController(ApplicationDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
		{

			IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();

			return Ok(_mapper.Map<List<VillaDTO>>(villaList));
		}

		[HttpGet("{id:int}", Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		public ActionResult<VillaDTO> GetVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var villa = _db.Villas.FirstOrDefault(u => u.Id == id);

			if (villa == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<VillaDTO>(villa));
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<VillaDTO> CreateVilla([FromBody] VillaCreateDTO createDTO)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
			{
				ModelState.AddModelError("CustomError", "Villa already Exits!");
				return BadRequest(ModelState);
			}

			if (createDTO == null)
			{
				return BadRequest(createDTO);
			}
			//if (villaDTO.Id > 0)
			//{
			//	return StatusCode(StatusCodes.Status500InternalServerError);
			//}

			Villa model = _mapper.Map<Villa>(createDTO);

			//Villa model = new()
			//{
			//	Amenity= createDTO.Amenity,
			//	Details= createDTO.Details,
			//	ImageUrl= createDTO.ImageUrl,
			//	Name	= createDTO.Name,
			//	Occupancy= createDTO.Occupancy,	
			//	Rate= createDTO.Rate,	
			//	Sqft= createDTO.Sqft
			//};
			_db.Villas.Add(model);
			_db.SaveChanges();

			return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
		}


		[HttpDelete("{id:int}", Name = "DeleteVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult DeleteVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.FirstOrDefault(u => u.Id == id);

			if (villa == null)
			{
				return NotFound();
			}
			_db.Villas.Remove(villa);
			_db.SaveChanges();

			return NoContent();
		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateVilla(int id, [FromBody] VillaDTOUpdate updateDTO)
		{
			if (updateDTO == null || id != updateDTO.Id)
			{
				return BadRequest();
			}

			Villa model = _mapper.Map<Villa>(updateDTO);

			//Villa model = new()
			//{
			//	Amenity = updateDTO.Amenity,
			//	Details = updateDTO.Details,
			//	Id = updateDTO.Id,
			//	ImageUrl = updateDTO.ImageUrl,
			//	Name = updateDTO.Name,
			//	Occupancy = updateDTO.Occupancy,
			//	Rate = updateDTO.Rate,
			//	Sqft = updateDTO.Sqft
			//};

			_db.Villas.Update(model);
			_db.SaveChanges();
			return NoContent();
		}

		[HttpPatch("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTOUpdate> patchDTO)
		{
			if (patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

			VillaDTOUpdate villaDTO = _mapper.Map<VillaDTOUpdate>(villa);

			//VillaDTOUpdate villaDTO = new()
			//{
			//	Amenity = villa.Amenity,
			//	Details = villa.Details,
			//	Id = villa.Id,
			//	ImageUrl = villa.ImageUrl,
			//	Name = villa.Name,
			//	Occupancy = villa.Occupancy,
			//	Rate = villa.Rate,
			//	Sqft = villa.Sqft
			//};

			if (villa == null)
			{
				return BadRequest();
			}
			patchDTO.ApplyTo(villaDTO, ModelState);

			Villa model = _mapper.Map<Villa>(villaDTO);

			_db.Villas.Update(model);
			_db.SaveChanges();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return NoContent();
		}
	}
}
