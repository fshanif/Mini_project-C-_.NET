using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    internal class MissingIdException : Exception
    {
        private string v;
        private object hostingUnitKey;

        public MissingIdException()
        {
        }

        public MissingIdException(string message) : base(message)
        {
        }

        public MissingIdException(string v, object hostingUnitKey)
        {
            this.v = v;
            this.hostingUnitKey = hostingUnitKey;
        }

        public MissingIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}