using RadwaMintaWebAPI.DTOs.ContactUs;
using RadwaMintaWebAPI.Interfaces;

namespace RadwaMintaWebAPI.Services
{
    public class ContactService(IEmailService _emailService, IConfiguration _configuration) : IContactService
    {
        public async Task<bool> SendContactMessageAsync(ContactMessageDto messageDto)
        {
            string recipientEmail = _configuration["ContactSettings:RecipientEmail"];
            string subject = $"New Contact Message from {messageDto.Name}";


            //string templatePath = Path.Combine("Templates", "ContactMessageTemplate.html"); 
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ContactMessageTemplate.html");



            string emailBodyTemplate;
            try
            {
                emailBodyTemplate = File.ReadAllText(templatePath); 
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: Email template not found at '{templatePath}'. Exception: {ex.Message}");
                emailBodyTemplate = $"<p>New Contact Message from {messageDto.Name}</p>" +
                                    $"<p>Phone Number: {messageDto.PhoneNumber}</p>" +
                                     $"<p>Email: {messageDto.email}</p>" +
                                    $"<p>Message: {messageDto.desc}</p>" +
                                    $"<p>Sent Date: {DateTime.Now}</p>";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading email template from '{templatePath}': {ex.Message}");
                emailBodyTemplate = $"<p>New Contact Message from {messageDto.Name}</p>" +
                                    $"<p>Phone Number: {messageDto.PhoneNumber}</p>" +
                                    $"<p>Email: {messageDto.email}</p>" +
                                    $"<p>Message: {messageDto.desc}</p>" +
                                    $"<p>Sent Date: {DateTime.Now}</p>";
            }


            string finalEmailBody = emailBodyTemplate
                .Replace("{{Name}}", messageDto.Name)
                .Replace("{{PhoneNumber}}", messageDto.PhoneNumber)
                .Replace("{{email}}", messageDto.email)
                .Replace("{{desc}}", messageDto.desc)
                .Replace("{{SentDate}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); 

            return await _emailService.SendEmailAsync(recipientEmail, subject, finalEmailBody);
        }
    }
}
