using System;

namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public NotFoundException(string name, object key)
            : base($"Requested \"{name}\" ({key}) was not found.") { }
        public bool Status { get; set; } = false;
    }
}
