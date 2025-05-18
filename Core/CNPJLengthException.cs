using System;

namespace CNPJValidatorV2.Core
{
    public class CNPJLengthException : Exception
    {
        private static readonly string message = "CNPJ must be 12 digits long";
        public CNPJLengthException() : base(message)
        {
        }
    }
}
