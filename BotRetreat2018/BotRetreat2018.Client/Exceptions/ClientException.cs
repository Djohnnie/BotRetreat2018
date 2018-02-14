using System;

namespace BotRetreat2018.Client.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException(String message) : base(message) { }
    }
}