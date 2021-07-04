using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThomsonReuters.Application;
using ThomsonReuters.Business.Entities;
using ThomsonReuters.Business.Interfaces;


namespace ThomsonReuters.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LegalCaseController : Controller
    {
        private readonly ILegalCaseService _legalCaseService;

        public LegalCaseController(ILegalCaseService legalCaseService)
        {
            _legalCaseService = legalCaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLegalCases([FromQuery] int pageSize = 10, [FromQuery] int page = 1, [FromQuery] string query = null)
        {
            try
            {
                var result = await _legalCaseService.GetAll(pageSize, page, query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{numberCase}")]
        public async Task<IActionResult> GetLegalCase(string numberCase)
        {
            try
            {
                var result = await _legalCaseService.GetByNumber(numberCase);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("LegalCase with this number was not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(LegalCase), 201)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] LegalCaseViewModel model)
        {
            if(!model.IsValid())
            {
                var errors = new List<string>();
                foreach (var error in model.ValidationResult.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }

                return BadRequest(errors);
            }
            else
            {
                return Ok(await _legalCaseService.Create(model));
            }
        }


        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateLegalCase([FromBody] LegalCaseViewModel produto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    return BadRequest(errors);
                }
                else
                {
                    var result = await _legalCaseService.Update(produto);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound("LegalCase with this number was not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{number}")]
        public async Task<IActionResult> DeleteCase(string caseNumber)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    return BadRequest(errors);
                }
                else
                {
                    var result = await _legalCaseService.Delete(caseNumber);
                    return result == true ? Ok(result) : NotFound("LegalCase with this number was not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
