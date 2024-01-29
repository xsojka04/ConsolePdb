using System;

namespace ConsolePdb.Exceptions
{
    public class KafkaBadFormatException : Exception
    {
        public KafkaBadFormatException(string message) : base(message)
        {
        }

        public KafkaBadFormatException() : base()
        {
        }
    }
}
