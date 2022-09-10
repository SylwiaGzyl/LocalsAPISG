using System;

namespace LocalsAPISG.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }

    }
}
