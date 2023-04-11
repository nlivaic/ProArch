using System;

namespace ProArch.CodingTest.Exceptions
{
    internal class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}
