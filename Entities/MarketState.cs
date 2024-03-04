namespace StockMarketWithSignalR.Entities
{
    public class MarketState : BaseEntity
    {
        public decimal TotalPrice { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
