using CNPJValidatorV2.Core;

namespace CNPJValidatorV2.Test
{
    public class CnpjValidatorTest
    {
        [Theory]
        [InlineData("12345678000195", true)]
        [InlineData("12345678000276", true)]
        [InlineData("12345678000357", true)]
        [InlineData("12345678000438", true)]
        [InlineData("12ABC34501DE35", true)]
        [InlineData("12ABC34501AB77", true)]
        [InlineData("12ABC34501CD82", true)]
        [InlineData("12ABC34501FG40", true)]
        [InlineData("12.345.678/0001-95", true)]
        [InlineData("12.345.678/0002-76", true)]
        [InlineData("12.345.678/0003-57", true)]
        [InlineData("12.345.678/0004-38", true)]
        [InlineData("12.ABC.345/01DE-35", true)]
        [InlineData("12.ABC.345/01AB-77", true)]
        [InlineData("12.ABC.345/01CD-82", true)]
        [InlineData("12.ABC.345/01FG-40", true)]
        public void ShouldValidateCnpj(string cnpj, bool expected)
        {
            var actual = CnpjValidator.IsValid(cnpj);
            Assert.Equal(actual, expected);
            Assert.True(actual);
        }

        [Theory]
        [InlineData("12345678000191", false)]
        [InlineData("12345678000271", false)]
        [InlineData("12345678000351", false)]
        [InlineData("12345678000431", false)]
        [InlineData("12ABC34501DE31", false)]
        [InlineData("12ABC34501AB71", false)]
        [InlineData("12ABC34501CD81", false)]
        [InlineData("12ABC34501FG41", false)]
        [InlineData("12.345.678/0001-91", false)]
        [InlineData("12.345.678/0002-71", false)]
        [InlineData("12.345.678/0003-51", false)]
        [InlineData("12.345.678/0004-31", false)]
        [InlineData("12.ABC.345/01DE-31", false)]
        [InlineData("12.ABC.345/01AB-71", false)]
        [InlineData("12.ABC.345/01CD-81", false)]
        [InlineData("12.ABC.345/01FG-41", false)]
        [InlineData("12.aBC.345/01DE-31", false)]
        [InlineData("12.abC.345/01AB-71", false)]
        [InlineData("12.abc.345/01CD-81", false)]
        [InlineData("12.abc.345/01fg-41", false)]
        public void ShouldNotValidateCnpj(string cnpj, bool expected)
        {
            var actual = CnpjValidator.IsValid(cnpj);
            Assert.Equal(actual, expected);
            Assert.False(actual);
        }

        [Theory]
        [InlineData("00000000000000", false)]
        [InlineData("11111111111111", false)]
        [InlineData("22222222222222", false)]
        [InlineData("33333333333333", false)]
        [InlineData("44444444444444", false)]
        [InlineData("55555555555555", false)]
        [InlineData("66666666666666", false)]
        [InlineData("77777777777777", false)]
        [InlineData("88888888888888", false)]
        [InlineData("99999999999999", false)]
        [InlineData("00.000.000/0000-00", false)]
        [InlineData("11.111.111/1111-11", false)]
        [InlineData("22.222.222/2222-22", false)]
        [InlineData("33.333.333/3333-33", false)]
        [InlineData("44.444.444/4444-44", false)]
        [InlineData("55.555.555/5555-55", false)]
        [InlineData("66.666.666/6666-66", false)]
        [InlineData("77.777.777/7777-77", false)]
        [InlineData("88.888.888/8888-88", false)]
        [InlineData("99.999.999/9999-99", false)]
        public void ShouldNotValidateCnpjIfCnpjIsInvalid(string cnpj, bool expected)
        {
            var actual = CnpjValidator.IsValid(cnpj);
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData("12345678000195", "12.345.678/0001-95")]
        [InlineData("12345678000276", "12.345.678/0002-76")]
        [InlineData("12345678000357", "12.345.678/0003-57")]
        [InlineData("12345678000438", "12.345.678/0004-38")]
        [InlineData("12ABC34501DE35", "12.ABC.345/01DE-35")]
        [InlineData("12ABC34501AB77", "12.ABC.345/01AB-77")]
        [InlineData("12ABC34501CD82", "12.ABC.345/01CD-82")]
        [InlineData("12ABC34501FG40", "12.ABC.345/01FG-40")]
        public void ShouldFormatCnpj(string cnpj, string expected)
        {
            var actual = cnpj.FormatCnpj();
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData("123456780001", "12345678000195")]
        [InlineData("123456780002", "12345678000276")]
        [InlineData("123456780003", "12345678000357")]
        [InlineData("123456780004", "12345678000438")]
        [InlineData("12ABC34501DE", "12ABC34501DE35")]
        [InlineData("12ABC34501AB", "12ABC34501AB77")]
        [InlineData("12ABC34501CD", "12ABC34501CD82")]
        [InlineData("12ABC34501FG", "12ABC34501FG40")]
        public void ShouldCalculateDv(string cnpj, string expected)
        {
            var actual = CnpjValidator.CalculateDv(cnpj);
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData("123456780001", "12345678000100")]
        [InlineData("123456780002", "12345678000201")]
        [InlineData("123456780003", "12345678000302")]
        [InlineData("123456780004", "12345678000403")]
        [InlineData("12ABC34501DE", "12ABC34501DE04")]
        [InlineData("12ABC34501AB", "12ABC34501AB05")]
        [InlineData("12ABC34501CD", "12ABC34501CD06")]
        [InlineData("12ABC34501FG", "12ABC34501FG07")]
        public void ShouldCalculateDvIsInvalid(string cnpj, string expected)
        {
            var actual = CnpjValidator.CalculateDv(cnpj);
            Assert.NotEqual(actual, expected);
        }

        [Theory]
        [InlineData("123456780001", "12.345.678/0001-95")]
        [InlineData("123456780002", "12.345.678/0002-76")]
        [InlineData("123456780003", "12.345.678/0003-57")]
        [InlineData("123456780004", "12.345.678/0004-38")]
        [InlineData("12ABC34501DE", "12.ABC.345/01DE-35")]
        [InlineData("12ABC34501AB", "12.ABC.345/01AB-77")]
        [InlineData("12ABC34501CD", "12.ABC.345/01CD-82")]
        [InlineData("12ABC34501FG", "12.ABC.345/01FG-40")]
        public void ShouldCalculateDvAndFormat(string cnpj, string expected)
        {
            var actual = CnpjValidator.CalculateDv(cnpj).FormatCnpj();
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData("123456780001", "12.345.678/0001-00")]
        [InlineData("123456780002", "12.345.678/0002-01")]
        [InlineData("123456780003", "12.345.678/0003-02")]
        [InlineData("123456780004", "12.345.678/0004-03")]
        [InlineData("12ABC34501DE", "12.ABC.345/01DE-04")]
        [InlineData("12ABC34501AB", "12.ABC.345/01AB-05")]
        [InlineData("12ABC34501CD", "12.ABC.345/01CD-05")]
        [InlineData("12ABC34501FG", "12.ABC.345/01FG-06")]
        [InlineData("12ABC34501FG", "12.ABC.345/01FG-AB")]
        public void ShouldCalculateDvAndFormatButIsInvalid(string cnpj, string expected)
        {
            var actual = CnpjValidator.CalculateDv(cnpj).FormatCnpj();
            Assert.NotEqual(actual, expected);
        }

        [Theory]
        [InlineData("1", "CNPJ must be 12 digits long")]
        [InlineData("12", "CNPJ must be 12 digits long")]
        [InlineData("12.3", "CNPJ must be 12 digits long")]
        [InlineData("12.34", "CNPJ must be 12 digits long")]
        [InlineData("12.345.", "CNPJ must be 12 digits long")]
        [InlineData("12.345.6", "CNPJ must be 12 digits long")]
        [InlineData("12.345.67", "CNPJ must be 12 digits long")]
        [InlineData("12.345.678", "CNPJ must be 12 digits long")]
        [InlineData("12.345.678/0", "CNPJ must be 12 digits long")]
        [InlineData("12.345.678/00", "CNPJ must be 12 digits long")]
        [InlineData("12.345.678/000", "CNPJ must be 12 digits long")]
        public void ShouldNotCalculateDvIfCnpjHasLessThan12Digtis(string cnpj, string expected)
        {
            var actual = Assert.Throws<CNPJLengthException>(() => CnpjValidator.CalculateDv(cnpj)).Message;
            Assert.Equal(actual, expected);
        }

        [Theory]
        [InlineData("12.345.678/0001-95", "12345678000195")]
        [InlineData("12.345.678/0002-76", "12345678000276")]
        [InlineData("12.345.678/0003-57", "12345678000357")]
        [InlineData("12.345.678/0004-38", "12345678000438")]
        [InlineData("12.ABC.345/01DE-35", "12ABC34501DE35")]
        [InlineData("12.ABC.345/01AB-77", "12ABC34501AB77")]
        [InlineData("12.ABC.345/01CD-82", "12ABC34501CD82")]
        [InlineData("12.ABC.345/01FG-40", "12ABC34501FG40")]
        public void ShouldSanitizeCnpj(string cnpj, string expected)
        {
            var actual = cnpj.SanitizeCnpj();
            Assert.Equal(actual, expected);
        }
    }
}