using IdentityService.Data;
using IdentityService.Features.Core;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace IdentityService.Features.Notifications
{
    public interface INotificationService
    {
        SendGridMessage BuildSurveyCompletedMessage();
        void ResolveRecipients(ref SendGridMessage mailMessage);
        Task<dynamic> SendAsync(SendGridMessage sendGridMessage);     
    }

    public class NotificationService: INotificationService
    {
        public NotificationService(Lazy<NotificationsConfiguration> lazyNotificationsConfiguration)
        {
            _sendGridClient = new SendGridClient(lazyNotificationsConfiguration.Value.SendGridApiKey);
        }

        public SendGridMessage BuildSurveyCompletedMessage()
        {
            var mailMessage = new SendGrid.Helpers.Mail.SendGridMessage();            
            var html = @"<html><body><h1>Test</h1></body></html>";
            mailMessage.HtmlContent = html;
            return mailMessage;
        }

        public void ResolveRecipients(ref SendGridMessage mailMessage)
        {
            mailMessage.AddTo(new EmailAddress("quinntynebrown@gmail.com"));            
        }

        public async Task<dynamic> SendAsync(SendGridMessage mailMessage)
        {
            return await _sendGridClient.SendEmailAsync(mailMessage);
        }

        private readonly IdentityServiceContext _context;
        private readonly ICache _cache;
        private readonly SendGridClient _sendGridClient;
    }
}
