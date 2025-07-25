namespace BlogApp.Core.Exceptions
{
    public class NoConnectivityAndNoLocalDataException: Exception
    {
        public NoConnectivityAndNoLocalDataException() { }
        public NoConnectivityAndNoLocalDataException(string message) : base(message) { }
        public NoConnectivityAndNoLocalDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
