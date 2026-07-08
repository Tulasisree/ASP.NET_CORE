using Microsoft.AspNetCore.Identity;
using DevSpot.Constants;

namespace DevSpot.RoleSeeder
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //Here Roles.ADMIN is created by me under Contants folder
                if (!await roleManager.RoleExistsAsync(Roles.ADMIN))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN));
                }

                if (!await roleManager.RoleExistsAsync(Roles.JOB_SEEKER))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.JOB_SEEKER));
                }

                if (!await roleManager.RoleExistsAsync(Roles.EMPLOYER))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.EMPLOYER));
                }
        }
    }
}