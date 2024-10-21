using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private ICommonService <BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;
        

        public BeerController(IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            [FromKeyedServices("beerService")] ICommonService <BeerDto, BeerInsertDto, BeerUpdateDto> beerService)
        {
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]

        public async Task<IEnumerable<BeerDto>> Get() =>
            await _beerService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetbyId(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }
        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_beerService.Validate(beerInsertDto))
            {
                return BadRequest(_beerService.Errors);
            }

            var beerDto = await _beerService.Add(beerInsertDto);
            
            return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetByName(string name, bool useStoredProcedure = false)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("El nombre de la cerveza no puede estar vacío.");
                }

                IEnumerable<BeerDto> beers;

                // Decide si usar el procedimiento almacenado o no
                if (useStoredProcedure)
                {
                    beers = await _beerService.SearchByNameStoredProcedure(name);
                }
                else
                {
                    beers = await _beerService.SearchByName(name);
                }

                if (beers == null || !beers.Any())
                {
                    return NotFound($"No se encontraron cervezas con el nombre: {name}");
                }

                return Ok(beers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las cervezas de la base de datos.");
            }
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (!_beerService.Validate(beerUpdateDto))
            {
                return BadRequest(_beerService.Errors);
            }

            var beerDto = await _beerService.Update(id, beerUpdateDto);
            return beerDto ==null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerService.Delete(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }
    }

}
