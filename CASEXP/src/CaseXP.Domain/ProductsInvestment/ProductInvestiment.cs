namespace CASEXP.CaseXP.Domain.ProductsInvestment;

public class ProductInvestiment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public decimal CurrentValue { get; set; }
    public DateTime? MaturityDate { get; set; }

    // Relationship: An investment productInvestiment belongs to a client
    public int ClientId { get; set; }
    public virtual Client.Client Client { get; set; }
}
