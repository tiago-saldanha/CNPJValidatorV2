using CNPJValidatorV2.Core;

namespace CNPJValidatorV2.Test
{
    public class CNPJValidatorTest
    {
        [Fact(DisplayName = "ShouldValidateCNPJ")]
        public void ShouldValidateCNPJ()
        {
            var cnpj = "12.345.678/0001-95";
            var expected = CNPJValidator.IsValid(cnpj);
            Assert.True(expected);
        }

        [Fact(DisplayName = "ShouldNotValidateCNPJ")]
        public void ShouldNotValidateCNPJ()
        {
            var cnpj = "12.345.678/0001-94";
            var expected = CNPJValidator.IsValid(cnpj);
            Assert.False(expected);
        }

        [Fact(DisplayName = "ShouldNotValidateCNPJIfCNPJIsInvalid")]
        public void ShouldNotValidateCNPJIfCNPJIsInvalid()
        {
            var cnpj = "11.111.111/1111-11";
            var expected = CNPJValidator.IsValid(cnpj);
            Assert.False(expected);
        }

        [Fact(DisplayName = "ShouldFormatCNPJ")]
        public void ShouldFormatCNPJ()
        {
            var cnpj = "12345678000195";
            var expected = cnpj.FormatCNPJ();
            Assert.Equal("12.345.678/0001-95", expected);
        }

        [Fact(DisplayName = "ShouldCalculateDV")]
        public void ShouldCalculateDV()
        {
            var cnpj = "12ABC34501DE";
            var expected = CNPJValidator.CalculateDV(cnpj);
            Assert.Equal("12ABC34501DE35", expected);
        }

        [Fact(DisplayName = "ShouldCalculateDVIsInvalid")]
        public void ShouldCalculateDVIsInvalid()
        {
            var cnpj = "12ABC34501DE";
            var expected = CNPJValidator.CalculateDV(cnpj);
            Assert.NotEqual("12ABC34501DE37", expected);
        }

        [Fact(DisplayName = "ShouldCalculateDVAndFormat")]
        public void ShouldCalculateDVAndFormat()
        {
            var cnpj = "12ABC34501DE";
            var expected = CNPJValidator.CalculateDV(cnpj).FormatCNPJ();
            Assert.Equal("12.ABC.345/01DE-35", expected);
        }

        [Fact(DisplayName = "ShouldCalculateDVAndFormatButIsInvalid")]
        public void ShouldCalculateDVAndFormatButIsInvalid()
        {
            var cnpj = "12ABC34501DE";
            var expected = CNPJValidator.CalculateDV(cnpj).FormatCNPJ();
            Assert.NotEqual("12.ABC.345/01DE-37", expected);
        }

        [Fact(DisplayName = "ShouldNotCalculateDVIfCNPJHasLessThan12Digtis")]
        public void ShouldNotCalculateDVIfCNPJHasLessThan12Digtis()
        {
            var cnpj = "12ABC34501D";
            var message = Assert.Throws<CNPJLengthException>(() => CNPJValidator.CalculateDV(cnpj)).Message;
            Assert.Equal("CNPJ must be 12 digits long", message);
        }

        [Fact(DisplayName = "ShouldSanitizeCNPJ")]
        public void ShouldSanitizeCNPJ()
        {
            var cnpj = "12.ABC.345/01DE-FG";
            var expected = cnpj.SanitizeCNPJ();
            Assert.Equal("12ABC34501DEFG", expected);
        }
    }
}