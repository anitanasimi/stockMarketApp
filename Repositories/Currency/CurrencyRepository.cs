using Microsoft.EntityFrameworkCore;
using StockMarketWithSignalR.Database;
using StockMarketWithSignalR.Entities;
using StockMarketWithSignalR.Enums;
using System.Linq;

namespace StockMarketWithSignalR.Repositories.Currency
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly StockMarketDb _db;

        public CurrencyRepository(StockMarketDb db)
        {
            _db = db;
        }

        public async Task<List<Entities.Currency>> GetAllCurrencies()
        {
            return await _db.Currencies.ToListAsync();
        }

        public async Task<Entities.Currency?> GetCurrencyById(Guid currencyId)
        {
            return await _db.Currencies.FindAsync(currencyId);
        }

        public async Task<AddResult> Add(Entities.Currency currency)
        {
            try
            {
                var isExists = await IsExists(currency.Title, currency.CurrencyCode);
                if (isExists)
                {
                    return AddResult.Duplicate;
                }

                await _db.Currencies.AddAsync(currency);
                await _db.SaveChangesAsync();
                return AddResult.Success;
            }
            catch
            {
                return AddResult.Failed;
            }
        }

        public async Task<UpdateResult> Update(Entities.Currency currency)
        {
            try
            {
                var c = await _db.Currencies.FindAsync(currency.Id);
                if (c is null)
                {
                    return UpdateResult.NotFound;
                }

                _db.Currencies.Update(currency);
                await _db.SaveChangesAsync();
                return UpdateResult.Success;
            }
            catch
            {
                return UpdateResult.Failed;
            }
        }

        public async Task<DeleteResult> Delete(Guid currencyId)
        {
            try
            {
                var c = await _db.Currencies.FindAsync(currencyId);
                if (c is null)
                {
                    return DeleteResult.NotFound;
                }

                _db.Currencies.Remove(c);
                await _db.SaveChangesAsync();
                return DeleteResult.Success;
            }
            catch
            {
                return DeleteResult.Failed;
            }
        }

        public async Task<bool> IsExists(string title, int code)
        {
            return await _db.Currencies.AnyAsync(c => c.Title == title && c.CurrencyCode == code);
        }

        /// <summary>
        /// its an random algorithm 
        /// </summary>
        /// <param name="currencyId"></param>
        /// <param name="marketStateId"></param>
        /// <returns></returns>
        public async Task UpdateCurrencyPrice(Guid currencyId, Guid marketStateId)
        {
            var currency = await GetCurrencyById(currencyId);
            if (currency is null)
            {
                return;
            }
            var currencyStatics = await _db
                .MarketStatistics
                .Where(m => m.CurrencyId == currency.Id)
                .ToListAsync();

            decimal buyCount = currencyStatics
                .Where(s => s.OperationType == OperationType.Buy)
                .Select(s=>s.Count)
                .Sum();
            decimal sellCount = currencyStatics
                .Where(s => s.OperationType == OperationType.Sell)
                .Select(s => s.Count)
                .Sum();

            var res = buyCount - sellCount;

            currency.Price += (res * currency.Coefficient);
            currency.MarketStateId = marketStateId;

            await Update(currency);
        }
    }
}
