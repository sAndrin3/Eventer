using Event_Management.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Extensions
{
    public static class AddMigration
    {

        public static IApplicationBuilder ApplyMigration (this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }

            return app;
        }
    }
}