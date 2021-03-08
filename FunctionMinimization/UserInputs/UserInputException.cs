using System;

namespace FunctionMinimization.UserInputs
{
    internal class UserInputException : Exception
    {
        public UserInputException(string message) : base(message) { }
    }
}
