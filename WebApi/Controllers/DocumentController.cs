using Api.BizSign.Core.Models;
using System.Security.Claims;
using Api.BizSign.Infrastructure.Repositories.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.BizSign.WebApi.Controllers;

[Route("api/document")]
public class DocumentController : Controller
{
    private readonly IDocumentRepository _repo;
     
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized(new { message = "Token inválido ou usuário não identificado" });
        
        
        var documents = await _repo.GetByOwnerIdAsync(userIdClaim);

        return Ok(documents);
    }
    
    
    [HttpPost("add")]
    public async Task<IActionResult> SaveDocument([FromForm] Document document)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized(new { message = "Token inválido ou usuário não identificado" });
        
        document.OwnerID = userIdClaim;
        
        await _repo.CreateAsync(document);
        
        return Ok(new { message = "Documento criado com sucesso" });
    }
}