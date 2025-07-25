namespace BlogApp.Core.Models
{
    public class AlertRequestedEventArgs
    {
        public required string Title { get; set; }
        public required string Message { get; set; }
        public string CancelButton { get; set; } = "OK";
        public string? AcceptButton { get; set; }
        public bool IsConfirmation { get; set; }
    }
}
