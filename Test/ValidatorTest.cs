using CNPJValidator.Core;

namespace Test
{
    public class ValidatorTest
    {
        [Fact(DisplayName = "ShouldValidadeCNPJ")]
        public void ShouldValidadeCNPJ()
        {
            var cnpj = "12.345.678/0001-95";
            var isValid = Validator.IsValid(cnpj);
            Assert.True(isValid);
        }

        [Fact(DisplayName = "ShouldNotValidadeCNPJ")]
        public void ShouldNotValidadeCNPJ()
        {
            var cnpj = "12.345.678/0001-94";
            var isValid = Validator.IsValid(cnpj);
            Assert.False(isValid);
        }

        [Fact(DisplayName = "ShouldFormatCNPJ")]
        public void ShouldFormatCNPJ()
        {
            var cnpj = "12345678000195";
            var formattedCNPJ = Validator.FormatCNPJ(cnpj);
            Assert.Equal("12.345.678/0001-95", formattedCNPJ);
        }
    }
}