namespace BlogApp.Core.Exceptions
{
    public class NoLocalDataException: Exception
    {
        public NoLocalDataException() { }
        public NoLocalDataException(string message) : base(message) { }
        public NoLocalDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
