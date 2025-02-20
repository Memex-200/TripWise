using Microsoft.AspNetCore.Mvc;
using TripWise.Application.Interfaces.Services;
using TripWise.Domain.Entities;

namespace TripWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportCompaniesController : ControllerBase
    {
        private readonly ITransportCompanyService _companyService;

        public TransportCompaniesController(ITransportCompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
            => Ok(await _companyService.GetAllCompaniesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            return company == null ? NotFound() : Ok(company);
        }
    }
}
