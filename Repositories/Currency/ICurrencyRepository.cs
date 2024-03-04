using StockMarketWithSignalR.Enums;

namespace StockMarketWithSignalR.Repositories.Currency;

public interface ICurrencyRepository
{
    Task<List<Entities.Currency>> GetAllCurrencies();

    Task<Entities.Currency?> GetCurrencyById(Guid currencyId);

    Task<AddResult> Add(Entities.Currency currency);

    Task<UpdateResult> Update(Entities.Currency currency);

    Task<DeleteResult> Delete(Guid currencyId);

    Task<bool> IsExists(string title,int code);

    Task UpdateCurrencyPrice(Guid currencyId,Guid marketStateId);
}