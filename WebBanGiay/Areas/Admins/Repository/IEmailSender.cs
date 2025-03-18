namespace WebBanGiay.Areas.Admins.Repository

{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);
	}
}
