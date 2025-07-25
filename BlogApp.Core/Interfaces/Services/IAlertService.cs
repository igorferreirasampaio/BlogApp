namespace BlogApp.Core.Interfaces.Services
{
    public interface IAlertService
    {
        Task SendAlertMessageAsync(Page page, string title, string message, string cancelButton = "OK");
        Task<bool> SendConfirmationMessageAsync(Page page, string title, string message, string acceptButton, string cancelButton);
    }
}
