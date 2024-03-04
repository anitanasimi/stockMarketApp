using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarketWithSignalR.Dtos.Market;
using StockMarketWithSignalR.Repositories.Market;

namespace StockMarketWithSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketRepository _marketRepository;

        public MarketController(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto request)
        {
            var transactionResult = await _marketRepository.DoTransaction(request);

            if (transactionResult)
            {
                return Ok(transactionResult);
            }

            return BadRequest(transactionResult);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMarket()
        {
            var transactionResult = await _marketRepository.GetMarket();
            return Ok(transactionResult);
        }
    }
}
