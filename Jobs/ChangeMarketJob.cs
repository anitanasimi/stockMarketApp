using Quartz;
using StockMarketWithSignalR.Dtos.Market;
using StockMarketWithSignalR.Entities;
using StockMarketWithSignalR.Repositories.Currency;
using StockMarketWithSignalR.Repositories.Market;

namespace StockMarketWithSignalR.Jobs
{
    /// <summary>
    /// This job is created for automatic transactions.
    /// It's designed to give you the feeling of a real market.
    /// </summary>
    public class ChangeMarketJob : IJob
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMarketRepository _marketRepository;

        public ChangeMarketJob(ICurrencyRepository currencyRepository, IMarketRepository marketRepository)
        {
            _currencyRepository = currencyRepository;
            _marketRepository = marketRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var currencies = await _currencyRepository.GetAllCurrencies();

            foreach (var currency in currencies)
            {
                var r = new Random();
                var value = r.Next(1,50);

                if (value % 2 == 0)
                {
                    var count = r.Next(1, 10);
                    await _marketRepository.DoTransaction(new CreateTransactionDto()
                    {
                        Count = count,
                        CurrencyId = currency.Id,
                        OperationType = OperationType.Buy
                    });
                }
                else
                {
                    var count = r.Next(1, 10);
                    await _marketRepository.DoTransaction(new CreateTransactionDto()
                    {
                        Count = count,
                        CurrencyId = currency.Id,
                        OperationType = OperationType.Sell
                    });
                }
            }
        }
    }
}
