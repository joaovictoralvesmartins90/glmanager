using Microsoft.AspNetCore.Identity;

namespace glmanager.Models;

public class Usuario: IdentityUser
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}
