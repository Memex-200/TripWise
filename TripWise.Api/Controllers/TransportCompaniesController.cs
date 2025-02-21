using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripWise.Application.Interfaces.Services;
using TripWise.Domain.Entities;

namespace TripWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 Requires authentication
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
    }
}
