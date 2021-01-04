namespace APIReactTemplate.Services.Identity.Interfaces
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<string> Login(string userName, string passWord);

        Task Logout(string jwt);
    }
}
