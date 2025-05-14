namespace IbkrTradeRepository.PortalApp.Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public string BrokerName { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string BaseCurrency { get; set; } = "EUR";
        public List<Trade> Trades { get; set; } = new();
        public List<CashTransaction> CashTransactions { get; set; } = new();
    }
}
