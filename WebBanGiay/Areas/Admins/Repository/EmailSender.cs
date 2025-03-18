using System.Net.Mail;
using System.Net;

namespace WebBanGiay.Areas.Admins.Repository
{
	public class EmailSender : IEmailSender
	{
		public async Task SendEmailAsync(string email, string subject, string message, bool isHtml = true)
		{
			try
			{
				using var client = new SmtpClient("smtp.gmail.com", 587)
				{
					EnableSsl = true,
					Credentials = new NetworkCredential("quangduy1997vn@gmail.com", "szxwwcxcgakqvxej")
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress("quangduy1997vn@gmail.com", "T1 Shop"),
					Subject = subject,
					Body = message,
					IsBodyHtml = isHtml
				};

				mailMessage.To.Add(email);

				await client.SendMailAsync(mailMessage);
			}
			catch (SmtpException smtpEx)
			{
				throw new Exception("Lỗi SMTP: " + smtpEx.Message, smtpEx);
			}
		}
	}
}
