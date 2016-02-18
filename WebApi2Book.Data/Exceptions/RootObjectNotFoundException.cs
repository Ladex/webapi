using System;

namespace WebApi2Book.Data.Exceptions
{
    [Serializable]
    public class RootObjectNotFoundException : Exception
    {
        public RootObjectNotFoundException(string message) : base(message)
        {
        }
    }

    public class ChildObjectNotFoundException : Exception
    {
        public ChildObjectNotFoundException(string message) : base(message)
        {
        }
    }
}