namespace IbkrTradeRepository.PortalApp.Domain
{
    public class OptionTrade
    {
        public Guid TradeId { get; set; }
        public Trade Trade { get; set; } = default!;

        public OptionType OptionType { get; set; }
        public decimal StrikePrice { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Multiplier { get; set; } = 100;
    }
}