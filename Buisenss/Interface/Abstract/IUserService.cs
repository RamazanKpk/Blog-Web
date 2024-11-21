using Entity.Concrate;
using System.Threading.Tasks;

namespace Buisenss.Interface.Abstract
{
    public interface IUserService
    {
        Task<User> UserLog(User user);
        bool CheckAuthorization(User user);
        User ValidateUser(string userName, string Password);
        Task<User> GetUserByName(string UserName);
        Task<User> CheckUserName(string UserName);

    }
}
