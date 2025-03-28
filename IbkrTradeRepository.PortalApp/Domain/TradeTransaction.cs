namespace IbkrTradeRepository.PortalApp.Domain
{
    public class TradeTransaction
    {
        public Guid Id { get; set; }

        public Guid TradeId { get; set; }
        public Trade Trade { get; set; } = default!;

        public DateTime ExecutionDate { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; } = "EUR";
        public decimal Price { get; set; }

        public string OrderType { get; set; } = "Market";
    }
}