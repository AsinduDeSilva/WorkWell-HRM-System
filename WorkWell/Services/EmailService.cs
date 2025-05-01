using MimeKit;
using System.Windows;
using MailKit.Net.Smtp;
using MailKit.Security;


namespace WorkWell.Services
{
    class EmailService
    {
        public static void SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("WorkWell", "workwell189@gmail.com"));
                message.To.Add(new MailboxAddress("Recipient Name", toAddress));
                message.Subject = subject;

                message.Body = new TextPart("html") // "html" or "plain" for text
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // Note: Only needed if the SMTP server requires authentication
                    client.Authenticate("workwell189@gmail.com", "amfetpfbmigpqiwc");

                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
