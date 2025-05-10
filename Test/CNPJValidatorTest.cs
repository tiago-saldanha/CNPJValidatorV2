using System;
using CNPJValidatorV2.Core;

namespace Test
{
    public class CNPJValidatorTest
    {
        [Fact(DisplayName = "ShouldValidadeCNPJ")]
        public void ShouldValidadeCNPJ()
        {
            var cnpj = "12.345.678/0001-95";
            var isValid = CNPJValidator.IsValid(cnpj);
            Assert.True(isValid);
        }

        [Fact(DisplayName = "ShouldNotValidadeCNPJ")]
        public void ShouldNotValidadeCNPJ()
        {
            var cnpj = "12.345.678/0001-94";
            var isValid = CNPJValidator.IsValid(cnpj);
            Assert.False(isValid);
        }

        [Fact(DisplayName = "ShouldFormatCNPJ")]
        public void ShouldFormatCNPJ()
        {
            var cnpj = "12345678000195";
            var formattedCNPJ = cnpj.FormatCNPJ();
            Assert.Equal("12.345.678/0001-95", formattedCNPJ);
        }

        [Fact(DisplayName = "ShouldCalculateDV")]
        public void ShouldCalculateDV()
        {
            var cnpj = "12ABC34501DE";
            var calculatedCNPJ = CNPJValidator.CalculateDV(cnpj);
            Assert.Equal("12ABC34501DE35", calculatedCNPJ);
        }

        [Fact(DisplayName = "ShouldCalculateDVIsInvalid")]
        public void ShouldCalculateDVIsInvalid()
        {
            var cnpj = "12ABC34501DE";
            var calculatedCNPJ = CNPJValidator.CalculateDV(cnpj);
            Assert.NotEqual("12ABC34501DE37", calculatedCNPJ);
        }

        [Fact(DisplayName = "ShouldCalculateDVAndFormat")]
        public void ShouldCalculateDVAndFormat()
        {
            var cnpj = "12ABC34501DE";
            var calculatedCNPJ = CNPJValidator.CalculateDV(cnpj).FormatCNPJ();
            Assert.Equal("12.ABC.345/01DE-35", calculatedCNPJ);
        }

        [Fact(DisplayName = "ShouldCalculateDVAndFormatButIsInvalid")]
        public void ShouldCalculateDVAndFormatButIsInvalid()
        {
            var cnpj = "12ABC34501DE";
            var calculatedCNPJ = CNPJValidator.CalculateDV(cnpj).FormatCNPJ();
            Assert.NotEqual("12.ABC.345/01DE-37", calculatedCNPJ);
        }

        [Fact(DisplayName = "ShouldNotCalculateDVIfCNPJLessThan12Digtis")]
        public void ShouldNotCalculateDVIfCNPJLessThan12Digtis()
        {
            var cnpj = "12ABC34501D";
            var message = Assert.Throws<ArgumentException>(() => CNPJValidator.CalculateDV(cnpj)).Message;
            Assert.Equal("CNPJ must be 12 digits long", message);
        }

        [Fact(DisplayName = "ShouldSanitizeCNPJ")]
        public void ShouldSanitizeCNPJ()
        {
            var cnpj = "12.ABC.345/01DE-FG";
            var expected = CNPJValidator.SanitizeCNPJ(cnpj);
            Assert.Equal("12ABC34501DEFG", expected);
        }
    }
}