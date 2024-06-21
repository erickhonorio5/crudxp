using System.ComponentModel.DataAnnotations;

namespace CASEXP.CaseXP.Application.UseCases.Gateways;

public class ClientRequestDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string CPF { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Address { get; set; }
  
}