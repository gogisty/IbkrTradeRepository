namespace TradeRepositoryAPI.IbkrResponses
{
    public class Trade
    {
        public int TradeId { get; set; }
        public string Currency { get; set; }

        public float TradePrice { get; set; }

        public int Quantity { get; set; }

        public DateTime TradeDate { get; set; }
    }
}
