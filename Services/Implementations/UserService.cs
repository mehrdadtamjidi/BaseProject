using Common.Utilities;
using Data.Repositories;
using Entities.Account;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Account;
using Services.Interfaces;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor

        private IRepository<User> userRepository;
        private IPasswordHelper passwordHelper;
        private IMailSender mailSender;
        private IViewRenderService renderView;

        public UserService(IUserRepository userRepository, IPasswordHelper passwordHelper, IMailSender mailSender, IViewRenderService renderView)
        {
            this.userRepository = userRepository;
            this.passwordHelper = passwordHelper;
            this.mailSender = mailSender;
            this.renderView = renderView;
        }

        #endregion

        #region User Section

        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.TableNoTracking.ToListAsync();
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if (IsUserExistsByEmail(register.Email))
                return RegisterUserResult.EmailExists;

            var user = new User
            {
                Email = register.Email.SanitizeText(),
                Address = register.Address.SanitizeText(),
                FirstName = register.FirstName.SanitizeText(),
                LastName = register.LastName.SanitizeText(),
                EmailActiveCode = Guid.NewGuid().ToString(),
                Password = passwordHelper.EncodePasswordMd5(register.Password)
            };

            await userRepository.AddAsync(user, CancellationToken.None);

            var body = await renderView.RenderToStringAsync("Email/ActivateAccount", user);

            mailSender.Send("mohammad1375ordo@gmail.com", "test", body);

            return RegisterUserResult.Success;
        }

        public bool IsUserExistsByEmail(string email)
        {
            return userRepository.TableNoTracking.Any(s => s.Email == email.ToLower().Trim());
        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO login)
        {
            var password = passwordHelper.EncodePasswordMd5(login.Password);

            var user = await userRepository.TableNoTracking
                .SingleOrDefaultAsync(s => s.Email == login.Email.ToLower().Trim() && s.Password == password);

            if (user == null) return LoginUserResult.IncorrectData;

            if (!user.IsActivated) return LoginUserResult.NotActivated;

            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await userRepository.TableNoTracking.SingleOrDefaultAsync(s => s.Email == email.ToLower().Trim());
        }

        public async Task<User> GetUserByUserId(long userId)
        {
            return await userRepository.GetByIdAsync(CancellationToken.None, userId);
        }

        public void ActivateUser(User user)
        {
            user.IsActivated = true;
            user.EmailActiveCode = Guid.NewGuid().ToString();
            userRepository.UpdateAsync(user, CancellationToken.None);
        }

        public Task<User> GetUserByEmailActiveCode(string emailActiveCode)
        {
            return userRepository.TableNoTracking.SingleOrDefaultAsync(s => s.EmailActiveCode == emailActiveCode);
        }

        #endregion

        #region dispose

        public void Dispose()
        {
            // userRepository.Dispose();
        }

        #endregion

    }
}