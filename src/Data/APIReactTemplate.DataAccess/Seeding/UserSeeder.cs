namespace APIReactTemplate.DataAccess.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using APIReactTemplate.Domain.Identity;

    using static APIReactTemplate.Common.Identity.IdentityConstants.AdminRoleName;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            string[] roles = new string[] { AdminRole };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(dbContext);

                if (!dbContext.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role));
                }
            }

            var user = new ApplicationUser
            {
                Email = "admin@mysite.com",
                NormalizedEmail = "ADMIN@MYSITE.COM",
                UserName = "admin@mysite.com",
                NormalizedUserName = "ADMIN@MYSITE.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            if (!dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "admin123");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(dbContext);
                var result = userStore.CreateAsync(user);
            }

            await AssignRoles(serviceProvider, user.Email, roles);

            await dbContext.SaveChangesAsync();
        }

        private static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUser> userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
