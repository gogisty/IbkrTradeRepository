namespace IbkrTradeRepository.PortalApp.Domain
{
    public class CashTransaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = default!;
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EUR";
        public CashTransactionType TransactionType { get; set; }
        public string? Description { get; set; }
    }
}