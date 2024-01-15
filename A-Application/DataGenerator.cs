using Bogus;
namespace A_Application
{
    public static class DataGenerator
    {
        private static readonly string[] currencyPairs = new[] { "EUR/UAH", "USD/UAH", "PLN/UAH", "EUR/USD", "PLN/EUR" };
        public static Faker<CurrencyPair> CreateCurrencyPairGenerator()
        {
            return new Faker<CurrencyPair>()
                .RuleFor(o => o.Currency, f => f.PickRandom(currencyPairs))
                .RuleFor(o => o.Price, f => f.Random.Decimal(0, 50))
                .RuleFor(o => o.TimeStamp, f => f.Date.Recent(200));
        }
    }
}
