namespace Event_Management.Extensions
{
    public static class AddAutheExtensions
    {

        public static WebApplicationBuilder addAuthorizationExtension (this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Roles", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Roles", "admin");
                });
            });
            return builder;
        }
    }
}