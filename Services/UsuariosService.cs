using glmanager.DTOs;
using glmanager.Models;
using glmanager.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace glmanager.Services;

public class UsuariosService : IUsuariosService
{
    private readonly UserManager<Usuario> userManager;

    public UsuariosService(UserManager<Usuario> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<Resultado<UsuarioResponse>> CadastrarAsync(UsuarioCadastroDto req)
    {
        if (!Cpf.EhValido(req.Cpf))
        {
            return Resultado<UsuarioResponse>.Falha("CPF inválido");
        }

        if(Cpf.EhValido(req.Cpf) && userManager.Users.Any(u => u.Cpf == req.Cpf))
        {
            return Resultado<UsuarioResponse>.Falha("CPF já cadastrado");
        }

        if (string.IsNullOrEmpty(req.NomeCompleto))
        {
            return Resultado<UsuarioResponse>.Falha("Nome completo obrigatório");
        }

        var usuario = new Usuario
        {
            Email = req.Email,
            NomeCompleto = req.NomeCompleto,
            DataNascimento = req.DataNascimento,
            UserName = req.Email,
            Cpf = req.Cpf
        };

        var criacao = await userManager.CreateAsync(usuario, req.Password);

        if (!criacao.Succeeded)
        {
            return Resultado<UsuarioResponse>.Falha(criacao.Errors.Select(e => e.Description));
        }

        await userManager.AddToRoleAsync(usuario, "Usuário");

        return Resultado<UsuarioResponse>.Ok(ParaResponse(usuario));
    }

    private static UsuarioResponse ParaResponse(Usuario u) => new(u.Id, u.Email!, u.NomeCompleto);
}
