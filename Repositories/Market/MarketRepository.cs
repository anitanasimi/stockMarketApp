using Microsoft.EntityFrameworkCore;
using StockMarketWithSignalR.Database;
using StockMarketWithSignalR.Dtos.Market;
using StockMarketWithSignalR.Entities;
using StockMarketWithSignalR.Enums;
using StockMarketWithSignalR.Repositories.Currency;

namespace StockMarketWithSignalR.Repositories.Market
{
    public class MarketRepository : IMarketRepository
    {
        private readonly StockMarketDb _db;
        private readonly ICurrencyRepository _currencyRepository;

        public MarketRepository(StockMarketDb db, ICurrencyRepository currencyRepository)
        {
            _db = db;
            _currencyRepository = currencyRepository;
        }

        private async Task<bool> UpdateMarket(MarketStatistic marketStatistic)
        {
            var ms = await AddMarketStatic(marketStatistic);
            if (ms is null)
            {
                return false;
            }

            var marketState = new MarketState();
            var isMarketStateExists = await IsMarketStateExistsByCurrencyId(marketStatistic.CurrencyId);
            if (!isMarketStateExists)
            {
                marketState = await AddMarketState(new MarketState()
                {
                    CurrencyId = marketStatistic.CurrencyId,
                    TotalPrice = 1_000_000 // default
                });
            }
            else
            {
                marketState = await _db.MarketStates.FirstOrDefaultAsync(m => m.CurrencyId == marketStatistic.CurrencyId);
            }

            var newMarketStateTotalPrice = await CalculateNewMarketStateTotalPrice(marketStatistic, marketState);
            var updateResult = await UpdateMarketStateTotalPrice(marketState, newMarketStateTotalPrice);
            if (!updateResult)
            {
                return false;
            }

            await _currencyRepository.UpdateCurrencyPrice(marketState.CurrencyId,marketState.Id);
            return true;
        }

        public async Task<bool> DoTransaction(CreateTransactionDto transactionDto)
        {
            return await UpdateMarket(new MarketStatistic()
            {
                CurrencyId = transactionDto.CurrencyId,
                OperationType = transactionDto.OperationType,
                Count = transactionDto.Count,
            });
        }

        public async Task<List<MarketDto>> GetMarket()
        {
            var result = await _db
                .MarketStates
                .Include(m => m.Currency)
                .Select(m => new MarketDto()
                {
                    Currency = new CurrencyObj()
                    {
                        Id = m.CurrencyId,
                        Title = m.Currency.Title,
                        Price = m.Currency.Price
                    },
                    Statics = new MarketStaticsObj()
                    {
                        TotalPrice = m.TotalPrice,
                        BuyCount = _db.MarketStatistics.Count(s=>s.CurrencyId == m.CurrencyId && s.OperationType == OperationType.Buy),
                        SellCount = _db.MarketStatistics.Count(s=>s.CurrencyId == m.CurrencyId && s.OperationType == OperationType.Sell),
                    }
                })
                .ToListAsync();

            return result;
        }

        private async Task<MarketState?> AddMarketState(MarketState marketState)
        {
            try
            {
                var ms = await _db.MarketStates.FirstOrDefaultAsync(m => m.CurrencyId == marketState.CurrencyId);
                if (ms is null)
                {
                    await _db.MarketStates.AddAsync(marketState);
                    await _db.SaveChangesAsync();
                }

                return marketState;
            }
            catch
            {
                return null;
            }
        }

        private async Task<MarketStatistic?> AddMarketStatic(MarketStatistic marketStatistic)
        {
            try
            {
                await _db.MarketStatistics.AddAsync(marketStatistic);
                await _db.SaveChangesAsync();

                return marketStatistic;
            }
            catch
            {
                return null;
            }
        }

        private async Task<bool> UpdateMarketStateTotalPrice(MarketState marketState, decimal newTotalPrice)
        {
            try
            {
                marketState.TotalPrice = newTotalPrice;

                _db.MarketStates.Update(marketState);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> IsMarketStateExistsByCurrencyId(Guid currencyId)
        {
            return await _db.MarketStates.AnyAsync(m => m.CurrencyId == currencyId);
        }

        private async Task<decimal> CalculateNewMarketStateTotalPrice(MarketStatistic marketStatistic, MarketState marketState)
        {
            var currency = await _currencyRepository.GetCurrencyById(marketStatistic.CurrencyId);
            if (currency is null)
            {
                return default;
            }

            var result = currency.Coefficient * (marketStatistic.Count * currency.Price);

            var newMarketStateTotalPrice = marketStatistic.OperationType switch
            {
                OperationType.Buy => marketState.TotalPrice + result,
                OperationType.Sell => marketState.TotalPrice - result,
                _ => default
            };

            return newMarketStateTotalPrice;
        }
    }
}
