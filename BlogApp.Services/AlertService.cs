using BlogApp.Core.Interfaces.Services;

namespace BlogApp.Services
{
    public class AlertService : IAlertService
    {
        public async Task SendAlertMessageAsync(Page page, string title, string message, string cancelButton = "OK")
        {
            await page.DisplayAlert(title, message, cancelButton);
        }

        public async Task<bool> SendConfirmationMessageAsync(Page page, string title, string message, string acceptButton, string cancelButton)
        {
          return await page.DisplayAlert(title,message,acceptButton, cancelButton);
        }
    }
}
