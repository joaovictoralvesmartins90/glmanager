using System;
using glmanager.DTOs;
using glmanager.Services;
using Microsoft.AspNetCore.Mvc;

namespace glmanager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController: ControllerBase
{
    private readonly IUsuariosService usuariosService;

    public UsuariosController(IUsuariosService usuariosService)
    {
        this.usuariosService = usuariosService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastrar(UsuarioCadastroDto req)
    {

        var resultado = await usuariosService.CadastrarAsync(req);
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);

        return Created($"/api/usuarios/{resultado.Valor?.Id}", new { resultado.Valor.Id, resultado.Valor.Email });
    }
}
