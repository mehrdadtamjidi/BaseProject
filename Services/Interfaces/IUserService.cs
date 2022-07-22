using Entities.Account;
using Services.DTOs.Account;

namespace Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<User>> GetAllUsers();

        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        bool IsUserExistsByEmail(string email);
        Task<LoginUserResult> LoginUser(LoginUserDTO login);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserId(long userId);
        void ActivateUser(User user);
        Task<User> GetUserByEmailActiveCode(string emailActiveCode);
    }
}