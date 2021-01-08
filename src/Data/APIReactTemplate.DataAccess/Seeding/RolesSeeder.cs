namespace APIReactTemplate.DataAccess.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using APIReactTemplate.Domain.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static APIReactTemplate.Common.Identity.IdentityConstants.AdminRoleName;
    using static APIReactTemplate.Common.Identity.IdentityConstants.UserRoleName;

    public class RolesSeeder : ISeeder
    {
        private readonly IEnumerable<string> rolesToSeed = new List<string>
        {
            AdminRole, UserRole,
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var role in this.rolesToSeed)
            {
                await SeedRoleAsync(roleManager, role);
            }
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
