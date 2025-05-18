using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CNPJValidatorV2.Core;

public static class CnpjValidator
{
    private static readonly int[] Weights = [2, 3, 4, 5, 6, 7, 8, 9];
    private static readonly string[] Invalids = ["00000000000000", "11111111111111", "22222222222222", "33333333333333", "44444444444444", "55555555555555", "66666666666666", "77777777777777", "88888888888888", "99999999999999"];
    private const string SanitizePattern = "[^A-Z0-9]";
    private const char ZeroChar = '0';

    public static string CalculateDv(string input)
    {
        input = input.SanitizeCnpj();

        if (input.Length != 12) throw new CNPJLengthException();

        input += CalculateDigit(input);
        input += CalculateDigit(input);

        return input;
    }

    public static bool IsValid(string input)
    {
        input = input.SanitizeCnpj();

        if (input.Length == 14 && !Invalids.Contains(input))
        {
            return CalculateDv(input[..12]) == input;
        }
        
        return false;
    }
    
    public static string SanitizeCnpj(this string input) => Regex.Replace(input, SanitizePattern, string.Empty);

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
