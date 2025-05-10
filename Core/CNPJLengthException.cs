namespace CNPJValidatorV2.Core
{
    public class CNPJLengthException : Exception
    {
        private static readonly string message = "CNPJ must be 12 digits long";
        public CNPJLengthException() : base(message)
        {
        }
        public CNPJLengthException(string message) : base(message)
        {
        }
        public CNPJLengthException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
