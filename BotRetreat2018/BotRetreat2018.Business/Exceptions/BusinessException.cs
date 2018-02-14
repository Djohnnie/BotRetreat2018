using System;

namespace BotRetreat2018.Business.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(String message, Exception innerException = null) : base(message, innerException) { }
    }
}