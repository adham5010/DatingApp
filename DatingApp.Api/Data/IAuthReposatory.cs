using System.Threading.Tasks;
using DatingApp.Api.Models;

namespace DatingApp.Api.Data {
    public interface IAuthReposatory {
        Task<User> Register(User User,string Password);

        Task<User> Login(string UserName,string Password);

        Task<bool> IsUserExists(string UserName);
    }
}