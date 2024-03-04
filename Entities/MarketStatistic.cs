namespace StockMarketWithSignalR.Entities
{
    public class MarketStatistic : BaseEntity
    {
        public int Count { get; set; }

        public OperationType OperationType { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }
    }

    public enum OperationType
    {
        Sell = 1,
        Buy = 2
    }
}
