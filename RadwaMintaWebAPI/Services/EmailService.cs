using RadwaMintaWebAPI.Interfaces;
using System.Net;
using System.Net.Mail;

namespace RadwaMintaWebAPI.Services
{
    public class EmailService(IConfiguration _configuration) : IEmailService
    {
      
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                    Credentials = new NetworkCredential(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderPassword"]),
                    EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]),
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderName"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            //catch (Exception ex)
            //{
            //    // Log the exception
            //    Console.WriteLine($"Error sending email: {ex.Message}");
            //    return false;
            //}
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex}");
                throw;
            }
        }
    
    }
}
