namespace IbkrTradeRepository.PortalApp.Domain
{
    public class Trade
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = default!;
        public DateTime TradeDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal TradePrice { get; set; }
        public decimal Proceeds { get; set; }
        public decimal Commission { get; set; }
        public required string Currency { get; set; }
        public TradeType TradeType { get; set; }
        public TradeDirection TradeDirection { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = default!;
        public OptionTradeDetails? OptionDetails { get; set; }
        public IbkrCode? Code { get; set; }
    }
}