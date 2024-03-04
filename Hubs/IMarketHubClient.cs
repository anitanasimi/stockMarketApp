namespace StockMarketWithSignalR.Hubs;

public interface IMarketHubClient
{
    Task ReceiveMarket(string message);

    Task DoTransaction();
    Task GetMarket();
}