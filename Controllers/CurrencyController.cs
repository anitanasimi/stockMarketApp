using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarketWithSignalR.Dtos.Currency;
using StockMarketWithSignalR.Entities;
using StockMarketWithSignalR.Repositories.Currency;

namespace StockMarketWithSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var result = await _currencyRepository.GetAllCurrencies();
            return Ok(result);
        }

        [HttpGet("{currencyId}")]
        public async Task<IActionResult> Get(Guid currencyId)
        {
            var result = await _currencyRepository.GetCurrencyById(currencyId);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyDto request)
        {
            var result = await _currencyRepository.Add(new Currency()
            {
                Title = request.Title,
                Price = request.Price,
                CurrencyCode = request.CurrencyCode,
                Coefficient = request.Coefficient
            });
            
            return Ok(result);
        }
    }
}
