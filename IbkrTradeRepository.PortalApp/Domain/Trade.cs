namespace IbkrTradeRepository.PortalApp.Domain
{
    public class Trade
    {
        public Guid Id { get; set; }

        public string Ticker { get; set; } = default!;
        public string? UnderlyingTicker { get; set; }

        public TradeType TradeType { get; set; }

        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }

        public int Quantity { get; set; }

        public decimal OpenPrice { get; set; }
        public decimal? ClosePrice { get; set; }

        public decimal? RealizedPnL { get; set; }
        public decimal Commission { get; set; }

        public Guid AccountId { get; set; }
        public Account? Account { get; set; }

        public OptionTrade? OptionDetails { get; set; }
        public List<TradeTransaction> Transactions { get; set; } = new();
    }
}