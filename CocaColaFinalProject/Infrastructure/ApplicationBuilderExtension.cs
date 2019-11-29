using CM.Data;
using CM.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CM.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CMContext>();
                context.Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();

                Task.Run(async () =>
                {
                    var adminName = "Administrator";

                    var exists = await roleManager.RoleExistsAsync(adminName);

                    if (!exists)
                    {
                        await roleManager.CreateAsync(new AppRole
                        {
                            Name = adminName
                        });
                    }

                    var managerName = "Manager";

                    var existsManager = await roleManager.RoleExistsAsync(managerName);

                    if (!existsManager)
                    {
                        await roleManager.CreateAsync(new AppRole
                        {
                            Name = managerName
                        });
                    }

                    var memberName = "Member";

                    var existsMember = await roleManager.RoleExistsAsync(memberName);

                    if (!existsMember)
                    {
                        await roleManager.CreateAsync(new AppRole
                        {
                            Name = memberName
                        });
                    }

                    var adminUser = await userManager.FindByNameAsync(adminName);

                    if (adminUser == null)
                    {
                        adminUser = new AppUser
                        {
                            UserName = "admin",
                            Email = "admin@admin.com"
                        };

                        await userManager.CreateAsync(adminUser, "admin12");
                        await userManager.AddToRoleAsync(adminUser, adminName);
                    }
                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}

