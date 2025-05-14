namespace IbkrTradeRepository.PortalApp.Domain
{
    public class OptionTradeDetails
    {
        public Guid TradeId { get; set; }
        public Trade Trade { get; set; } = default!;
        public decimal StrikePrice { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public OptionType OptionType { get; set; }
        public string UnderlyingSymbol { get; set; } = default!;
        public int Multiplier { get; set; } = 100;
    }
}