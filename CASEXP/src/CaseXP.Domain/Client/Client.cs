using System.ComponentModel.DataAnnotations.Schema;
using CASEXP.CaseXP.Domain.ProductsInvestment;

namespace CASEXP.CaseXP.Domain.Client;

[Table("Client")]
public class Client
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    
    public virtual ICollection<ProductInvestiment> InvestmentProducts { get; set; }
}

