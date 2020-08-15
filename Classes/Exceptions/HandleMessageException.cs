namespace Classes.Exceptions
{
    using System;

    public class HandleMessageException : Exception
    {
        public HandleMessageException()
        {
        }

        public HandleMessageException(string message)
            : base(message)
        {
        }

        public HandleMessageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
