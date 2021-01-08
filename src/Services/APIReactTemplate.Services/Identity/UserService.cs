namespace APIReactTemplate.Services.Identity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using APIReactTemplate.Common.ApplicationSettings;
    using APIReactTemplate.DataAccess;
    using APIReactTemplate.Domain.Identity;
    using APIReactTemplate.Services.Identity.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using static APIReactTemplate.Common.Identity.IdentityConstants.UserRoleName;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        private readonly SignInManager<ApplicationUser> signManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJWTService jwtService;
        private readonly JwtSettings jwtSettings;

        public UserService(
            ApplicationDbContext db,
            SignInManager<ApplicationUser> signManager,
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            IJWTService jwtService)
        {
            this.db = db;
            this.signManager = signManager;
            this.userManager = userManager;
            this.jwtSettings = jwtSettings.Value;
            this.jwtService = jwtService;
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = this.db.Users.SingleOrDefault(user => user.UserName == userName);

            if (user == null)
            {
                return string.Empty;
            }

            var result = await this.signManager.PasswordSignInAsync(userName, password, true, false);

            if (result.Succeeded)
            {
                return await this.GenerateJWT(user);
            }

            return string.Empty;
        }

        public async Task Logout(string jwt)
        {
            await this.jwtService.DeactivateJWT(jwt);
        }

        public async Task<string> Register(ApplicationUser newUser, string password)
        {
           var result = await this.userManager.CreateAsync(newUser, password);

           if (result.Succeeded)
           {
               var addToRoleResult = await this.userManager.AddToRoleAsync(newUser, UserRole);
               if (addToRoleResult.Succeeded)
               {
                  return await this.GenerateJWT(newUser);
               }
            }

           return string.Empty;
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var userClaims = await this.userManager.GetClaimsAsync(user);
            var userRoles = await this.userManager.GetRolesAsync(user);
            claims.AddRange(userClaims);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private async Task<string> GenerateJWT(ApplicationUser user)
        {
            var expirationDate = DateTime.UtcNow.AddDays(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(await this.GetUserClaims(user)),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
            ),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            await this.jwtService.AddJWT(jwt, user.Id, expirationDate);

            return jwt;
        }
    }
}
