namespace CASEXP.CaseXP.Domain.Transaction;

public interface ITransactionRepository
{
    Transaction GetById(int id);
    void Add(Transaction transaction);
    void Update(Transaction transaction);
    
}
