using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CNPJValidatorV2.Core;

public static class CNPJValidator
{
    private static readonly int[] weights = [2, 3, 4, 5, 6, 7, 8, 9];
    
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

        if (cnpj.Length != 14)
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

        var builder = new StringBuilder(18);

        if (cnpj.Length >= 14)
        {
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
