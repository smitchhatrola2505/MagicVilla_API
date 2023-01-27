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
	[Route("api/VillaNumberApi")]
	[ApiController]
	public class VillaNumberApiController : ControllerBase
	{
		protected APIResponse _response;
		private readonly IVillaNumberRepository _dbVillaNumber;
		private readonly IVillaRepository _dbVilla;
		private readonly IMapper _mapper;
		public VillaNumberApiController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
		{
			_dbVillaNumber = dbVillaNumber;
			_mapper = mapper;
			this._response = new();
			_dbVilla = dbVilla;
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetVillasNumber()
		{
			try
			{
				IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync(includeProperties:"Villa");
				_response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
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

		[HttpGet("{id:int}", Name = "GetVillaNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
		{

			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);

				if (villaNumber == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;

					return NotFound(_response);
				}
				_response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
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
		public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreatedDTO createDTO)
		{

			try
			{
				if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createDTO.VillaNo) != null)
				{
					ModelState.AddModelError("ErrorMessage", "Villa already Exits!");
					return BadRequest(ModelState);
				}

				if (await _dbVilla.GetAsync(u => u.Id == createDTO.VillaID) == null)
				{
					ModelState.AddModelError("ErrorMessage", "Villa Id Is Invalid!");
					return BadRequest(ModelState);
				}

				if (createDTO == null)
				{
					return BadRequest(createDTO);
				}
				

				VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDTO);

				
				_dbVillaNumber.CreateAsync(villaNumber);

				_response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetVilla", new { id = villaNumber.VillaNo }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessage = new List<string> { ex.ToString() };
			}
			return _response;
		}


		[HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
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
				var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);

				if (villaNumber == null)
				{
					return NotFound();
				}
				await _dbVillaNumber.RemoveAsync(villaNumber);
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

		[HttpPut("{id:int}", Name = "UpdateVillaNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdatedDTO updateDTO)
		{
			try
			{
				if (updateDTO == null || id != updateDTO.VillaNo)
				{
					return BadRequest();
				}

				if (await _dbVilla.GetAsync(u => u.Id == updateDTO.VillaID) == null)
				{
					ModelState.AddModelError("ErrorMessage", "Villa Id Is Invalid!");
					return BadRequest(ModelState);
				}

				VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);

				await _dbVillaNumber.UpdateAsync(model);

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
	}
}
