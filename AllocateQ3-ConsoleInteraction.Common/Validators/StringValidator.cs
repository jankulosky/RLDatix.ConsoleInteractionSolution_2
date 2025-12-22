using AllocateQ3_ConsoleInteraction.Common.Exceptions;

namespace AllocateQ3_ConsoleInteraction.Common.Validators
{
    public static class StringValidator
    {
        public static string CheckNullOrEmpty(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new InputException("Invalid input");
            }

            return input;
        }
    }
}
