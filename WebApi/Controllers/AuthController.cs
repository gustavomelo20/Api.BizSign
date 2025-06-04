using Api.BizSign.Core.Models;
using Api.BizSign.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Api.BizSign.Exceptions;
using Api.BizSign.WebApi.Request;
using FluentValidation;

namespace Api.BizSign.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromServices] IValidator<AuthRequest> validator,
        [FromBody] AuthRequest request
    )
    {
        try
        {
            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.PasswordHash
            };

            await service.Register(user);

            return Ok(new { message = "Usuário criado com sucesso" });
        }
        catch (EmailInUseException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromServices] IValidator<AuthRequest> validator,
        [FromBody] AuthRequest request
    )
    {
        try
        {
            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.PasswordHash
            };

            var token = await service.LoginAsync(user);
            return Ok(new { token });
        }
        catch (InvalidCredentialsException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}