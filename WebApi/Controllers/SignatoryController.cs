
using Api.BizSign.Core.Models;
using Api.BizSign.Infrastructure.Repositories.Contract;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace Api.BizSign.WebApi.Controllers;

[Route("api/signatory")]
public class SignatoryController : Controller
{
    private readonly ISignatoryRepository _repo;
    private readonly IConfiguration _config;

    public SignatoryController(ISignatoryRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        var signatory = new List<object>
        {
            new {
                id = 1,
                nome = "Gustavo Silva",
                email = "joao.silva@email.com",
                telefone = "(11) 99999-1111",
                empresa = "Tech Solutions",
                cargo = "Diretor",
                status = "Ativo",
                documentos = 12
            },
            new {
                id = 2,
                nome = "Maria Santos",
                email = "maria.santos@email.com",
                telefone = "(11) 99999-2222",
                empresa = "Inovação Corp",
                cargo = "Gerente",
                status = "Ativo",
                documentos = 8
            },
            new {
                id = 3,
                nome = "Pedro Costa",
                email = "pedro.costa@email.com",
                telefone = "(11) 99999-3333",
                empresa = "StartUp XYZ",
                cargo = "CEO",
                status = "Inativo",
                documentos = 5
            },
            new {
                id = 4,
                nome = "Ana Oliveira",
                email = "ana.oliveira@email.com",
                telefone = "(11) 99999-4444",
                empresa = "Consultoria ABC",
                cargo = "Consultora",
                status = "Ativo",
                documentos = 15
            }
        };

        var response = new { signatory };

        return Ok(response);
    }
    
    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> Register([FromBody] Signatory signatory)
    {
        //signatory.Id = Guid.NewGuid();
        await _repo.CreateAsync(signatory);
        
        return Ok(new { message = "Signatario criado com sucesso" });
    }
}