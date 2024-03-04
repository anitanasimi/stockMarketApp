using System.Text;
using Microsoft.AspNetCore.SignalR;
using StockMarketWithSignalR.Dtos;
using StockMarketWithSignalR.Dtos.Market;
using StockMarketWithSignalR.Repositories.Currency;
using StockMarketWithSignalR.Repositories.Market;

namespace StockMarketWithSignalR.Hubs
{
    public sealed class MarketHub : Hub
    {
        #region constructor

        private readonly IMarketRepository _marketRepository;
        private readonly ICurrencyRepository _currencyRepository;

        private readonly string _automaticTransaction;

        public MarketHub(IMarketRepository marketRepository,
            ICurrencyRepository currencyRepository)
        {
            _marketRepository = marketRepository;
            _currencyRepository = currencyRepository;
            _automaticTransaction = "admin-bot";
        }

        #endregion

        public override async Task OnConnectedAsync()
        {
           
        }

        public async Task JoinMarket(UserConnection userConnection)
        {
            await Clients.All.SendAsync("ReceiveMessage", _automaticTransaction,
                $"{userConnection.Username} is in market.",$"{DateTime.Now.ToString("o")}");
        }

        public async Task JoinTransactions(CreateTransactionDto createTransactionDto)
        {
            var isSuccess = await _marketRepository.DoTransaction(createTransactionDto);

            var message = string.Empty;
            var messageStatics = string.Empty;

            if (isSuccess)
            {
                var market = await _marketRepository.GetMarket(Guid.Parse(createTransactionDto.CurrencyId));
                message = $"success transaction on {market?.Currency.Title} make new price : {market?.Currency.Price} {Environment.NewLine}";
                messageStatics = $"{market?.Currency.Title} => buys count : {market?.Statics.BuyCount} and sells count : {market?.Statics.SellCount} with total market price : {market?.Statics.TotalPrice} {Environment.NewLine}";
            }

            await Clients.All.SendAsync("ReceiveTransaction", message,messageStatics, isSuccess);
        }
    }
}
