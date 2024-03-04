using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockMarketWithSignalR.Dtos.Market;
using StockMarketWithSignalR.Hubs;
using StockMarketWithSignalR.Repositories.Market;

namespace StockMarketWithSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IHubContext<MarketHub> _marketHub;

        public MarketController(IMarketRepository marketRepository,
            IHubContext<MarketHub> marketHub)
        {
            _marketRepository = marketRepository;
            _marketHub = marketHub;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto request)
        {
            var transactionResult = await _marketRepository.DoTransaction(request);

            if (transactionResult)
            {
                await _marketHub.Clients.All.SendAsync("DoTransaction");
                await _marketHub.Clients.All.SendAsync("GetMarket");
                return Ok(transactionResult);
            }

            return BadRequest(transactionResult);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMarket()
        {
            var market = await _marketRepository.GetMarket();

            await _marketHub.Clients.All.SendAsync("GetMarket", market);

            return Ok();
        }
    }
}
