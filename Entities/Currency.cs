namespace StockMarketWithSignalR.Entities
{
    public class Currency : BaseEntity
    {
        public string Title { get; set; }

        public int CurrencyCode { get; set; }

        public decimal Coefficient { get; set; }

        public ICollection<MarketStatistic>? MarketStatistics { get; set; }
        public Guid? MarketStateId { get; set; }
        public MarketState? MarketState { get; set; }
    }
}
