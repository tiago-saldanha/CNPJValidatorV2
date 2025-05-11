using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CNPJValidatorV2.Core;

public static class CNPJValidator
{
    private static readonly int[] weights = [2, 3, 4, 5, 6, 7, 8, 9];
    private static readonly string[] invalids = ["00000000000000", "11111111111111", "22222222222222", "33333333333333", "44444444444444", "55555555555555", "66666666666666", "77777777777777", "88888888888888", "99999999999999"];

    public static string CalculateDV(string cnpj)
    {
        cnpj = cnpj.SanitizeCNPJ();

        if (cnpj.Length != 12) throw new CNPJLengthException();

        cnpj += CalculateDigit(cnpj);
        cnpj += CalculateDigit(cnpj);

        return cnpj;
    }

    public static bool IsValid(string cnpj)
    {
        cnpj = cnpj.SanitizeCNPJ();

        if (cnpj.Length != 14 || invalids.Any(x => x == cnpj))
            return false;

        var calculate = CalculateDV(cnpj[..12]);
        if (calculate.Length != 14)
            return false;

        return calculate == cnpj;
    }
    
    public static string SanitizeCNPJ(this string cnpj) => Regex.Replace(cnpj, "[^a-zA-Z0-9]", string.Empty);

    public static string FormatCNPJ(this string cnpj)
    {
        cnpj = cnpj.SanitizeCNPJ();

        if (cnpj.Length >= 14)
        {
            var builder = new StringBuilder(18);
            builder.Append(cnpj[..2]).Append('.')
                   .Append(cnpj[2..5]).Append('.')
                   .Append(cnpj[5..8]).Append('/')
                   .Append(cnpj[8..12]).Append('-')
                   .Append(cnpj[12..]);

            return builder.ToString();
        }

        return cnpj;
    }

    private static int CalculateDigit(string cnpj)
    {
        var sum = cnpj
            .Reverse()
            .Select((c, i) => (c - 48) * weights[i % weights.Length])
            .Sum();

        var remainder = sum % 11;
        return remainder > 1 ? 11 - remainder : 0;
    }
}
