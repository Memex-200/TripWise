using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripWise.Application.Interfaces.Services;
using TripWise.Domain.Entities;

namespace TripWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransportCompaniesController : ControllerBase
    {
        private readonly ITransportCompanyService _companyService;

        public TransportCompaniesController(ITransportCompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await _companyService.GetAllCompaniesAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
