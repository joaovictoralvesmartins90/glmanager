using System;

namespace glmanager.DTOs;

public class Resultado<T>
{
    public bool Sucesso { get; set; }
    public T? Valor { get; }
    public IReadOnlyList<string> Erros { get; }

    public Resultado(bool sucesso, T? valor, IReadOnlyList<string> erros)
    {
        Sucesso = sucesso;
        Valor = valor;
        Erros = erros;
    }

    public static Resultado<T> Ok(T valor)
    {
        return new Resultado<T>(true, valor, []);
    }

    public static Resultado<T> Falha(params string[] erros)
    {
        return new Resultado<T>(false, default, erros);
    }

    public static Resultado<T> Falha(IEnumerable<string> erros)
    {
        return new Resultado<T>(false, default, erros.ToList());
    }
}
