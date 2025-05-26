using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CNPJValidatorV2.Core;

/// <summary>
/// Fornece métodos utilitários para sanitização, validação, cálculo de dígitos verificadores
/// e formatação de números de CNPJ (Cadastro Nacional da Pessoa Jurídica), com ou sem formatação.
/// 
/// Suporta CNPJs alfanuméricos contendo letras maiúsculas e dígitos, conforme regras da Receita Federal.
/// </summary>
public static class CnpjValidator
{
    private static readonly int[] Weights = [2, 3, 4, 5, 6, 7, 8, 9];
    private static readonly string[] Invalids = ["00000000000000", "11111111111111", "22222222222222", "33333333333333", "44444444444444", "55555555555555", "66666666666666", "77777777777777", "88888888888888", "99999999999999"];
    private const string SanitizePattern = "[^A-Z0-9]";
    private const char ZeroChar = '0';

    /// <summary>
    /// Calcula e retorna os dois dígitos verificadores de um CNPJ com base nos seus 12 primeiros caracteres alfanuméricos.
    /// </summary>
    /// <param name="input">
    /// Os 12 primeiros caracteres de um CNPJ alfanumérico (com ou sem formatação), contendo apenas letras maiúsculas e dígitos.
    /// </param>
    /// <exception cref="CNPJLengthException">
    /// Lançada se o CNPJ não tiver exatamente 12 caracteres válidos após a sanitização.
    /// </exception>
    /// <returns>O CNPJ completo (14 caracteres), sem formatação, com os dígitos verificadores calculados.</returns>
    public static string CalculateDv(string input)
    {
        input = input.SanitizeCnpj();

        if (input.Length != 12) throw new CNPJLengthException();

        input += CalculateDigit(input);
        input += CalculateDigit(input);

        return input;
    }

    /// <summary>
    /// Verifica se um CNPJ alfanumérico (com ou sem formatação) é válido, incluindo os dígitos verificadores.
    /// </summary>
    /// <param name="input">
    /// CNPJ alfanumérico (com ou sem formatação), contendo apenas letras maiúsculas e dígitos.
    /// </param>
    /// <returns>
    /// <c>true</c> se o CNPJ for válido (incluindo os dígitos verificadores); caso contrário, <c>false</c>.
    /// </returns>
    public static bool IsValid(string input)
    {
        input = input.SanitizeCnpj();

        if (input.Length == 14 && !Invalids.Contains(input))
        {
            return CalculateDv(input[..12]) == input;
        }
        
        return false;
    }

    /// <summary>
    /// Remove todos os caracteres inválidos de um CNPJ, mantendo apenas letras maiúsculas (A–Z) e dígitos (0–9).
    /// </summary>
    /// <param name="input">CNPJ com ou sem formatação.</param>
    /// <returns>CNPJ sanitizado contendo apenas letras maiúsculas e dígitos.</returns>
    public static string SanitizeCnpj(this string input) => Regex.Replace(input, SanitizePattern, string.Empty);

    /// <summary>
    /// Formata um CNPJ alfanumérico no padrão "00.000.000/0000-00", assumindo que ele tenha 14 caracteres válidos.
    /// </summary>
    /// <param name="input">CNPJ com ou sem formatação, contendo letras maiúsculas e/ou dígitos.</param>
    /// <returns>O CNPJ formatado ou, se inválido, o valor original.</returns>
    public static string FormatCnpj(this string input)
    {
        input = input.SanitizeCnpj();

        if (input.Length != 14) return input;

        var builder = new StringBuilder(18);
        builder.Append(input[..2]).Append('.')
               .Append(input[2..5]).Append('.')
               .Append(input[5..8]).Append('/')
               .Append(input[8..12]).Append('-')
               .Append(input[12..]);

        return builder.ToString();
    }

    /// <summary>
    /// Calcula um dígito verificador (DV) para um CNPJ alfanumérico com base nos 12 ou 13 primeiros caracteres,
    /// utilizando o algoritmo definido pela Receita Federal do Brasil.
    /// </summary>
    /// <param name="input">
    /// Sequência parcial do CNPJ (12 ou 13 caracteres alfanuméricos), onde cada caractere é convertido em valor numérico
    /// subtraindo 48 de seu código ASCII. A sequência é então processada da direita para a esquerda, multiplicando-se
    /// cada valor por pesos de 2 a 9 de forma cíclica.
    /// </param>
    /// <returns>
    /// Retorna um número inteiro entre 0 e 9, correspondente ao dígito verificador calculado por módulo 11:
    /// se o resto da divisão for menor que 2, retorna 0; caso contrário, retorna 11 - resto.
    /// </returns>
    /// <remarks>
    /// O processo segue as regras descritas no manual oficial da Receita Federal para cálculo do DV de CNPJ
    /// (inclusive para CNPJs alfanuméricos), considerando:
    /// - Conversão de cada caractere com base em seu valor ASCII (valor = ASCII - 48)
    /// - Aplicação de pesos de 2 a 9 de forma cíclica, da direita para a esquerda
    /// - Soma ponderada, seguida do cálculo do módulo 11
    /// </remarks>
    private static int CalculateDigit(string input)
    {
        var sum = input
            .Reverse()
            .Select((c, i) => (c - ZeroChar) * Weights[i % Weights.Length])
            .Sum();

        var remainder = sum % 11;
        return remainder > 1 ? 11 - remainder : 0;
    }
}
