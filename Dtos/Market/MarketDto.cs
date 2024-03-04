namespace StockMarketWithSignalR.Dtos.Market
{
    public class MarketDto
    {
        public CurrencyObj Currency { get; set; }

        public MarketStaticsObj Statics { get; set; }
    }

    public record CurrencyObj
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public record MarketStaticsObj
    {
        public int BuyCount { get; set; }
        public int SellCount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
