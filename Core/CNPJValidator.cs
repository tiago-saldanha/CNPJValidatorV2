using System.Text.RegularExpressions;

namespace CNPJValidatorV2.Core;

public static class CNPJValidator
{
    private static readonly int[] weights = [2, 3, 4, 5, 6, 7, 8, 9];
    
    public static string CalculateDV(string cnpj)
    {
        cnpj = SanitizeCNPJ(cnpj);

        if (cnpj.Length != 12)
            throw new ArgumentException("CNPJ must be 12 digits long");

        cnpj += CalculateDigit(cnpj);
        cnpj += CalculateDigit(cnpj);

        return cnpj;
    }

    public static bool IsValid(string cnpj)
    {
        cnpj = SanitizeCNPJ(cnpj);

        if (cnpj.Length != 14)
            return false;

        var copy = cnpj[..12];

        var calculate = CalculateDV(copy);
        if (calculate.Length != 14)
            return false;

        return calculate == cnpj;
    }
    
    public static string SanitizeCNPJ(this string cnpj) => Regex.Replace(cnpj, "[^a-zA-Z0-9]", string.Empty);

    public static string FormatCNPJ(this string cnpj)
    {
        cnpj = SanitizeCNPJ(cnpj);

        return cnpj.Length >= 14
            ? $"{cnpj[..2]}.{cnpj[2..5]}.{cnpj[5..8]}/{cnpj[8..12]}-{cnpj[12..]}"
            : cnpj;
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
