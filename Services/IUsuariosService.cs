using System;
using glmanager.DTOs;

namespace glmanager.Services;

public interface IUsuariosService
{
    Task<Resultado<UsuarioResponse>> CadastrarAsync(UsuarioCadastroDto req);
}
