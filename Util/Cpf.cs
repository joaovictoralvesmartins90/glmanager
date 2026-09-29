namespace glmanager.Util;

// Validacao/Cpf.cs
public static class Cpf
{
    public static bool EhValido(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        var digitos = Normalizar(cpf);

        if (digitos.Length != 11)
            return false;

        // Sequências como 000.000.000-00 e 111.111.111-11 passam no cálculo, mas são inválidas
        if (digitos.All(c => c == digitos[0]))
            return false;

        var primeiro = CalcularDigito(digitos, 9);   // usa os 9 primeiros dígitos
        var segundo = CalcularDigito(digitos, 10);   // usa os 9 primeiros + o 1º verificador

        return digitos[9] - '0' == primeiro
            && digitos[10] - '0' == segundo;
    }

    /// <summary>Remove pontos, traço e espaços. Use esta forma para salvar no banco.</summary>
    public static string Normalizar(string cpf)
        => new(cpf.Where(char.IsAsciiDigit).ToArray());

    public static string Formatar(string cpf)
    {
        var d = Normalizar(cpf);
        return d.Length == 11
            ? $"{d[..3]}.{d[3..6]}.{d[6..9]}-{d[9..]}"
            : cpf;
    }

    private static int CalcularDigito(string digitos, int quantidade)
    {
        // Pesos decrescentes: 10..2 para o 1º dígito, 11..2 para o 2º
        var soma = 0;
        for (var i = 0; i < quantidade; i++)
            soma += (digitos[i] - '0') * (quantidade + 1 - i);

        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
