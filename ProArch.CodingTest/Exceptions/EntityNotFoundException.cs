using System;

namespace ProArch.CodingTest.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}
