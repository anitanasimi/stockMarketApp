namespace StockMarketWithSignalR.Dtos.Currency
{
    public class CreateCurrencyDto
    {
        public string Title { get; set; }

        public int CurrencyCode { get; set; }

        public decimal Price { get; set; }

        public decimal Coefficient { get; set; }
    }
}
