namespace Jobs.API.Modules.Users
{
    public  static class UsersModule
    {
        public static void AddUsersModule(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<EmailService>();
        }
    }
}
