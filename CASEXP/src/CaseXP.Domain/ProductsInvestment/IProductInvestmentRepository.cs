namespace CASEXP.CaseXP.Domain.ProductsInvestment;

public interface IProductInvestmentRepository
{
    ProductInvestiment GetById(int id);
    void Add(ProductInvestiment productInvestiment);
    void Update(ProductInvestiment productInvestiment);
    void Delete(int id);
}
