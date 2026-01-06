using SendGrid;
using SendGrid.Helpers.Mail;

namespace Authentication.Services
{
    public class EmailService
    {
        private readonly string _apiKey;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _logger = logger;

            if (configuration == null)
            {
                _logger.LogError("Configuration is null. Cannot initialize EmailService.");
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiKey = configuration["Email:SendGrid:ApiKey"];
            _fromEmail = configuration["Email:SendGrid:From"] ?? "no-reply@lainlot.com";
            _fromName = configuration["Email:SendGrid:FromName"] ?? "Lainlot Support";

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                _logger.LogWarning("SendGrid API key is missing or empty.");
            }

            _logger.LogInformation("SendGrid config loaded:");
            _logger.LogInformation("From: {FromEmail}", _fromEmail);
            _logger.LogInformation("FromName: {FromName}", _fromName);
            _logger.LogInformation("API Key starts with: {ApiKeyStart}", _apiKey?.Substring(0, 5) ?? "null");
        }

        public async Task SendEmail(string toEmail, string subject, string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                _logger.LogError("SendGrid API key is not set. Aborting email send.");
                throw new InvalidOperationException("SendGrid API key is missing.");
            }

            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent: htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Body.ReadAsStringAsync();
                _logger.LogError("SendGrid error: {Error}", error);
                throw new Exception($"SendGrid failed: {error}");
            }

            _logger.LogInformation("Email sent to {ToEmail} with subject '{Subject}'", toEmail, subject);
        }

        public async Task SendVerificationEmail(string toEmail, string confirmationUrl)
        {
            var subject = "Confirm your registration";
            var body = $"""
            <p>Hello,</p>
            <p>Please confirm your email by clicking the link below:</p>
            <p><a href="{confirmationUrl}">Confirm Email</a></p>
            <p>If you did not register, just ignore this email.</p>
            """;

            await SendEmail(toEmail, subject, body);
        }
    }
}