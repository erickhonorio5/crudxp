using CASEXP.CaseXP.Domain.ProductsInvestment;

namespace CASEXP.CaseXP.Domain.Transaction;

public class Transaction
{
    public int Id { get; set; }
    public string Type { get; set; } // "Buy" or "Sell"
    public DateTime TransactionDate { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }

    // Relationship: A transaction is performed on an investment productInvestiment by a client
    public int InvestmentProductId { get; set; }
    public virtual ProductInvestiment ProductInvestiment { get; set; }

    public int ClientId { get; set; }
    public virtual Client.Client Client { get; set; }
}
