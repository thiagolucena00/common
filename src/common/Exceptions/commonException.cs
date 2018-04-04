using System;
using System.Runtime.Serialization;

namespace common.Exceptions
{
    /// <summary>
    ///     Base exception type for those are thrown by the system for application specific exceptions.
    /// </summary>
    [Serializable]
    public class CommonException : Exception
    {
        /// <summary>
        ///     Creates a new <see cref="CommonException" /> object.
        /// </summary>
        public CommonException()
        {
        }

        /// <summary>
        ///     Creates a new <see cref="CommonException" /> object.
        /// </summary>
        public CommonException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="CommonException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CommonException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="CommonException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public CommonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}