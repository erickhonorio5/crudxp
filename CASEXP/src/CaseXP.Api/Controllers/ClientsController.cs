using CASEXP.CaseXP.Application.UseCases.Gateways;
using CASEXP.CaseXP.Domain.Client;
using CASEXP.CaseXP.Domain.ProductsInvestment;

namespace CASEXP.CaseXP.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
    private readonly IProductInvestmentRepository _investmentRepository;

    public ClientsController(IClientRepository clientRepository, IProductInvestmentRepository productInvestmentRepository)
    {
        _clientRepository = clientRepository;
        _investmentRepository = productInvestmentRepository;
    }

    // GET: api/clients
    [HttpGet]
    public IEnumerable<Client> Get()
    {
        return _clientRepository.GetAll();
    }

    // GET: api/clients/5
    [HttpGet("{id}", Name = "GetClient")]
    public ActionResult<Client> Get(int id)
    {
        var client = _clientRepository.GetById(id);
        if (client == null)
        {
            return NotFound();
        }
        return client;
    }

    // POST: api/clients
    [HttpPost]
    public ActionResult<Client> Post([FromBody] ClientRequestDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var client = new Client
        {
            Name = dto.Name,
            CPF = dto.CPF,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address
            
        };
        try
        {
            _clientRepository.Add(client);
            return CreatedAtRoute("GetClient", new { id = client.Id }, client);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
        
    }
    

    
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ClientRequestDTO dto)
    {
        
        var existingClient = _clientRepository.GetById(id);
        if (existingClient == null)
        {
            return NotFound();
        }

        try
        {
            
            existingClient.Name = dto.Name;
            existingClient.CPF = dto.CPF;
            existingClient.Email = dto.Email;
            existingClient.Phone = dto.Phone;
            existingClient.Address = dto.Address;
            

            
            _clientRepository.Update(existingClient);

            
            return NoContent();
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var client = _clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            
            _clientRepository.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    
}
