using Microsoft.AspNetCore.SignalR;
using StockMarketWithSignalR.Dtos.Market;
using StockMarketWithSignalR.Repositories.Currency;
using StockMarketWithSignalR.Repositories.Market;

namespace StockMarketWithSignalR.Hub
{
    public class MarketHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IMarketRepository _marketRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public MarketHub(IMarketRepository marketRepository, ICurrencyRepository currencyRepository)
        {
            _marketRepository = marketRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task SendMarket(CancellationToken cancellationToken)
        {
            var market = await _marketRepository.GetMarket();
            await Clients.All.SendAsync("SendMarket",market,cancellationToken);
        }

        public async Task DoTransaction(CreateTransactionDto transactionDto,CancellationToken cancellationToken)
        {
            var currency = await _currencyRepository.GetCurrencyById(transactionDto.CurrencyId);
            var result = new
            {
                CurrencyTitle = currency?.Title,
                Count = transactionDto.Count,
                OperationType = transactionDto.OperationType
            };
            await Clients.All.SendAsync("DoTransaction", result, cancellationToken);
        }
    }
}
