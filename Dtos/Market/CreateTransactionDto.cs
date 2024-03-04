using StockMarketWithSignalR.Entities;

namespace StockMarketWithSignalR.Dtos.Market
{
    public class CreateTransactionDto
    {
        public Guid CurrencyId { get; set; }

        public int Count { get; set; }

        public OperationType OperationType { get; set; }
    }
}
