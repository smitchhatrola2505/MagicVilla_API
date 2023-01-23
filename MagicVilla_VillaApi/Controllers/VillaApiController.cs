using AutoMapper;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.Dto;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{
	[Route("api/VillaApi")]
	[ApiController]
	public class VillaApiController : ControllerBase
	{
		protected APIResponse _response;
		private readonly IVillaRepository _dbVila;
		private readonly IMapper _mapper;
		public VillaApiController(IVillaRepository dbVila, IMapper mapper)
		{
			_dbVila = dbVila;
			_mapper = mapper;
			this._response = new();
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetVillas()
		{
			try
			{
				IEnumerable<Villa> villaList = await _dbVila.GetAllAsync();
				_response.Result = _mapper.Map<List<VillaDTO>>(villaList);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = new List<string> { ex.ToString() };
			}
			return _response;
		}

		[HttpGet("{id:int}", Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		public async Task<ActionResult<APIResponse>> GetVilla(int id)
		{

			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				var villa = await _dbVila.GetAsync(u => u.Id == id);

				if (villa == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;

					return NotFound(_response);
				}
				_response.Result = _mapper.Map<VillaDTO>(villa);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = new List<string> { ex.ToString() };
			}
			return _response;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<APIResponse> CreateVilla([FromBody] VillaCreateDTO createDTO)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			try
			{
				if (_dbVila.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
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

				Villa villa = _mapper.Map<Villa>(createDTO);

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
				_dbVila.CreateAsync(villa);

				_response.Result = _mapper.Map<VillaDTO>(villa);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = new List<string> { ex.ToString() };
			}
			return _response;
		}


		[HttpDelete("{id:int}", Name = "DeleteVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
		{
			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var villa = await _dbVila.GetAsync(u => u.Id == id);

				if (villa == null)
				{
					return NotFound();
				}
				await _dbVila.RemoveAsync(villa);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = new List<string> { ex.ToString() };
			}
			return _response;
		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaDTOUpdate updateDTO)
		{
			try
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

				await _dbVila.UpdateAsync(model);

				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = new List<string> { ex.ToString() };
			}
			return _response;
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
			var villa = _dbVila.GetAsync(u => u.Id == id, tracked: false);

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

			_dbVila.UpdateAsync(model);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return NoContent();
		}
	}
}
