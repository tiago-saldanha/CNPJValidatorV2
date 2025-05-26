using System;

namespace CNPJValidatorV2.Core
{
    /// <summary>
    /// Exceção lançada quando o CNPJ possui um tamanho inválido.
    /// </summary>
    public class CNPJLengthException : Exception
    {
        private static readonly string message = "CNPJ must be 12 digits long";

        /// <summary>
        /// Inicializa uma nova instância de <see cref="CNPJLengthException"/>.
        /// </summary>
        public CNPJLengthException() : base(message)
        {
        }
    }
}
