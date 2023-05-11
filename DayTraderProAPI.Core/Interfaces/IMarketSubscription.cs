
namespace DayTraderProAPI.Core.Interfaces
{
    public interface IMarketSubscription
    {
        Task<decimal> GetSpotPrice(string currencyPair);
        void SubscribeToMarketData(Action<string> onDataReceived);
    }
}
