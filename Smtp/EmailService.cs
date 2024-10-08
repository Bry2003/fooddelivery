

using System.Net;
using System.Net.Mail;

public class EmailService
{
    private readonly SmtpClient _smtpClient;

    public EmailService()
    {
        _smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("aa02b39e963df5", "9bb27e8ad5e283"),
            EnableSsl = true
        };
    }

    public void SendEmail(string toEmail, string subject, string body)
    {
        var mailMessage = new MailMessage("from@example.com", toEmail)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        _smtpClient.Send(mailMessage);
        Console.WriteLine("Email sent to: " + toEmail);
    }
}
