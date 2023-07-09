using MailKit.Net.Smtp;
using MimeKit;

namespace phone_shop_server.Util
{
    public interface IMailUtil
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public Response(string? message, bool? success)
            {
                Message = message;
                Success = success;
            }
        }
        Task<IMailUtil.Response> SendMailAsync(string? to, string? subject, string? content);
    }
    public class MailUtil : IMailUtil
    {
        private readonly IConfiguration _configuration;
        public MailUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IMailUtil.Response> SendMailAsync(string? to, string? subject, string? content)
        {
            var htmlBody = new BodyBuilder();
            htmlBody.HtmlBody = content;
            string displayMail = _configuration.GetSection("SendMailService:Email").Value;
            string displayName = _configuration.GetSection("SendMailService:DisplayName").Value;
            var email = new MimeMessage();
            email.To.Add(MailboxAddress.Parse(to));
            email.Sender = new MailboxAddress(displayName, displayMail);
            email.From.Add(new MailboxAddress(displayName, displayMail));
            email.Subject = subject;
            email.Body = htmlBody.ToMessageBody();
            using var smtpClient = new SmtpClient();
            try
            {
                smtpClient.Connect(_configuration.GetSection("SendMailService:Host").Value, Int32.Parse(_configuration.GetSection("SendMailService:Port").Value), MailKit.Security.SecureSocketOptions.StartTls);
                smtpClient.Authenticate(_configuration.GetSection("SendMailService:Email").Value, _configuration.GetSection("SendMailService:Password").Value);
                var result = await smtpClient.SendAsync(email);

                smtpClient.Disconnect(true);
                return new IMailUtil.Response("send mail success", true);
            }
            catch (Exception ex)
            {
                smtpClient.Disconnect(true);
                return new IMailUtil.Response(ex.Message, false);
            }
        }
    }
}
}
