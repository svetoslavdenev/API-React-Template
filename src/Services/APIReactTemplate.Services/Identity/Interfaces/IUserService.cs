namespace APIReactTemplate.Services.Identity.Interfaces
{
    using System.Threading.Tasks;

    using APIReactTemplate.Domain.Identity;

    public interface IUserService
    {
        Task<string> Register(ApplicationUser newUser, string password);

        Task<string> Login(string userName, string passWord);

        Task Logout(string jwt);
    }
}
