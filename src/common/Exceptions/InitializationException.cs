using System;
using System.Runtime.Serialization;

namespace common.Exceptions
{
    public class InitializationException : CommonException
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public InitializationException()
        {
        }

        /// <summary>
        ///     Constructor for serializing.
        /// </summary>
        public InitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public InitializationException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public InitializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}