using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyOSBB.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public bool MailSent { get; set; } = false;
        public Task SendEmailAsync(string email, string subject, string message)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                client.EnableSsl = false;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                ///// Realhost
                client.Port = 25; // 465;
                client.Host = "scp.realhost.com.ua";
                client.Credentials = new NetworkCredential("admin@vysoft.top", "ce1poxhukandrgwfmyqj");
                MailAddress from = new MailAddress("admin@vysoft.top", "Admin ", Encoding.UTF8);
                MailAddress to = new MailAddress(email);
                ///// Realhost

                MailMessage mail = new MailMessage();
                mail.From = from;
                mail.To.Add(to);
                mail.IsBodyHtml = true;
                mail.Body = message;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;

                try
                {
                    client.Send(mail);
                }
                catch (Exception exc)
                {
                    throw new ApplicationException(exc.Message);
                }
            }

            return Task.CompletedTask;
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                //Console.WriteLine("[{0}] Send canceled.", token);
                //_logger.LogInformation("User changed their password successfully.");
            }
            if (e.Error != null)
            {
                //Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                //Console.WriteLine("Message sent.");
            }
            MailSent = true;
        }
    }
}
