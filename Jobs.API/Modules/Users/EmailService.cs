namespace Jobs.API.Modules.Users
{
    public class EmailService
    {
        public async Task SendEmailConfirmationAsync(string email, string confirmationUrl)
        {
            Console.WriteLine($"To: {email}\nBody: ${confirmationUrl}");
        }
    }
}
