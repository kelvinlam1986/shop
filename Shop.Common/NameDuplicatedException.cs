using System;

namespace Shop.Common
{
    public class NameDuplicatedException : Exception
    {
        public NameDuplicatedException()
        {
        }

        public NameDuplicatedException(string message) : base(message)
        {
        }

        public NameDuplicatedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
