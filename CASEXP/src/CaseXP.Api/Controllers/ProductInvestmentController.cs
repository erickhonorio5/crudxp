using CASEXP.CaseXP.Domain.ProductsInvestment;
using Microsoft.AspNetCore.Mvc;

namespace CASEXP.CaseXP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductInvestmentController : ControllerBase
{
    private readonly ProductInvestmentService _productInvestmentService;

    public ProductInvestmentController(ProductInvestmentService productInvestmentService)
    {
        _productInvestmentService = productInvestmentService;
    }

    [HttpPost("buy")]
    public IActionResult BuyInvestment(int clientId, int investmentProductId, int quantity, decimal pricePerUnit)
    {
        try
        {
            _productInvestmentService.BuyInvestment(clientId, investmentProductId, quantity, pricePerUnit);
            return Ok("Investment purchased successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("sell")]
    public IActionResult SellInvestment(int clientId, int investmentProductId, int quantity, decimal pricePerUnit)
    {
        try
        {
            _productInvestmentService.SellInvestment(clientId, investmentProductId, quantity, pricePerUnit);
            return Ok("Investment sold successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
