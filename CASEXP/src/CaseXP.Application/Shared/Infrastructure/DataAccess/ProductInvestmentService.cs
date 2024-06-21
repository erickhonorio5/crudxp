using CASEXP.CaseXP.Domain.Client;
using CASEXP.CaseXP.Domain.Transaction;

namespace CASEXP.CaseXP.Domain.ProductsInvestment;

public class ProductInvestmentService
{
    private readonly IProductInvestmentRepository _productInvestmentRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ITransactionRepository _transactionRepository;

    public ProductInvestmentService(IProductInvestmentRepository productInvestmentRepository,
                                    IClientRepository clientRepository,
                                    ITransactionRepository transactionRepository)
    {
        _productInvestmentRepository = productInvestmentRepository;
        _clientRepository = clientRepository;
        _transactionRepository = transactionRepository;
    }

    // Method to perform a purchase of an investment productInvestiment by a client
    public void BuyInvestment(int clientId, int investmentProductId, int quantity, decimal pricePerUnit)
    {
        // Check if the client exists
        var client = _clientRepository.GetById(clientId);
        if (client == null)
        {
            throw new ApplicationException($"Client with ID {clientId} not found.");
        }

        // Check if the investment productInvestiment exists and belongs to the client
        var investmentProduct = _productInvestmentRepository.GetById(investmentProductId);
        if (investmentProduct == null)
        {
            throw new ApplicationException($"Investment productInvestiment with ID {investmentProductId} not found.");
        }

        if (investmentProduct.ClientId != clientId)
        {
            throw new ApplicationException($"Client {clientId} does not own investment productInvestiment {investmentProductId}.");
        }

        // Perform the purchase: create a new buy transaction
        var transaction = new Transaction.Transaction
        {
            Type = "Buy",
            TransactionDate = DateTime.UtcNow,
            Quantity = quantity,
            PricePerUnit = pricePerUnit,
            ClientId = clientId,
            InvestmentProductId = investmentProductId
        };

        // Update the current value of the investment productInvestiment
        investmentProduct.CurrentValue += quantity * pricePerUnit;

        // Save the transaction and update the investment productInvestiment
        _transactionRepository.Add(transaction);
        _productInvestmentRepository.Update(investmentProduct);
    }

    // Method to perform the sale of an investment productInvestiment by a client
    public void SellInvestment(int clientId, int investmentProductId, int quantity, decimal pricePerUnit)
    {
        // Check if the client exists
        var client = _clientRepository.GetById(clientId);
        if (client == null)
        {
            throw new ApplicationException($"Client with ID {clientId} not found.");
        }

        // Check if the investment productInvestiment exists and belongs to the client
        var investmentProduct = _productInvestmentRepository.GetById(investmentProductId);
        if (investmentProduct == null)
        {
            throw new ApplicationException($"Investment productInvestiment with ID {investmentProductId} not found.");
        }

        if (investmentProduct.ClientId != clientId)
        {
            throw new ApplicationException($"Client {clientId} does not own investment productInvestiment {investmentProductId}.");
        }

        // Check if there is enough quantity of the investment productInvestiment to sell
        if (investmentProduct.CurrentValue < quantity * pricePerUnit)
        {
            throw new ApplicationException($"Not enough quantity of investment productInvestiment {investmentProductId} for sale.");
        }

        // Perform the sale: create a new sell transaction
        var transaction = new Transaction.Transaction
        {
            Type = "Sell",
            TransactionDate = DateTime.UtcNow,
            Quantity = quantity,
            PricePerUnit = pricePerUnit,
            ClientId = clientId,
            InvestmentProductId = investmentProductId
        };

        // Update the current value of the investment productInvestiment
        investmentProduct.CurrentValue -= quantity * pricePerUnit;

        // Save the transaction and update the investment productInvestiment
        _transactionRepository.Add(transaction);
        _productInvestmentRepository.Update(investmentProduct);
    }
}

