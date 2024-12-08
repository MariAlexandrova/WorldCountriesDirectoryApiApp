using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldCountriesDirectoryApiApp.Model.Scenarious;
using WorldCountriesDirectoryApiApp.Model.Entity;
using WorldCountriesDirectoryApiApp.Model.Exception;
using static WorldCountriesDirectoryApiApp.Api.Messages.ApiMessages;

namespace WorldCountriesDirectoryApiApp.Api.Controllers
{
    // CountryController - контроллер для работы со странами
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryScenarios _scenarios;
        public CountryController(CountryScenarios scenarios) 
        {
            _scenarios = scenarios;
        }
        // GET /api/country - получить записи о всех странах
        [HttpGet]
        public async Task<IActionResult> GetAllCountriesAsync()
        {
            return Ok(await _scenarios.ListAllAsync());
        }

        // GET /api/country/{code} - получить страну по коду
        [HttpGet("{code}")]
        public async Task<IActionResult> GetCountryByCodeAsync(string code)
        {
            try
            {
                // 200
                return Ok(await _scenarios.GetByCodeAsync(code));
            }
            catch (CountryCodeFormatException ex)
            {
                // 400
                return BadRequest(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryNotFoundException ex)
            {
                // 404
                return NotFound(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
        }

        // POST /api/country - добавить новую страну
        [HttpPost]
        public async Task<IActionResult> AddCountryAsync([FromBody] Country country)
        {
            try
            {
                await _scenarios.AddAsync(country);
                // 201
                return Created();
            }
            catch (CountryCodeFormatException ex)
            {
                // 400
                return BadRequest(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryCodeDuplicatedException ex)
            {
                // 409
                return Conflict(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
        }
        // PATCH /api/country/{code} - обновить значение короткого и полного имени страны
        [HttpPatch("{code}")]
        public async Task<IActionResult> UpdateCountryAsync(string code, [FromBody] UpdateCountryDto updateCountryDto)
        {
            try
            {
                var updatedCountry = new Country
                {
                    ShortName = updateCountryDto.ShortName,
                    FullName = updateCountryDto.FullName,
                    IsoAlpha2 = code
                };

                var result = await _scenarios.EditAsync(code, updatedCountry);
                return Ok(result);
            }
            catch (CountryCodeFormatException ex)
            {
                // 400
                return BadRequest(new { error = ex.Message });
            }
            catch (CountryNotFoundException ex)
            {
                // 404
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // 409
                return Conflict(new { error = ex.Message });
            }
        }
        [HttpDelete("{code}")]
        public async Task<IActionResult> RemoveByCodeAsync(string code)
        {
            try
            {
                await _scenarios.DeleteAsync(code);
                return NoContent();
            }
            catch (CountryCodeFormatException ex)
            {
                // 400
                return BadRequest(new { error = ex.Message });
            }
            catch (CountryNotFoundException ex)
            {
                // 404
                return NotFound(new { error = ex.Message });
            }
        }

    }
}
