using System;

namespace AllocateQ3_ConsoleInteraction.Common.Exceptions
{
    public class InputException : Exception
    {
        public InputException(string message) : base(message) { }
    }
}
