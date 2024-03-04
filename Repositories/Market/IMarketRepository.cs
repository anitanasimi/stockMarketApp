using StockMarketWithSignalR.Dtos.Market;
using StockMarketWithSignalR.Entities;
using StockMarketWithSignalR.Enums;

namespace StockMarketWithSignalR.Repositories.Market;

public interface IMarketRepository
{
    Task<bool> DoTransaction(CreateTransactionDto transactionDto);

    Task<List<MarketDto>> GetMarket();

    Task<MarketDto?> GetMarket(Guid currencyId);
}