using Common.Utilities;
using Data.Context;
using Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return await Table.Where(p => p.Email == username && p.Password == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }

        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }

        public Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken)
        {
            //user.SecurityStamp = Guid.NewGuid();
            return UpdateAsync(user, cancellationToken);
        }



        //public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        //{
        //    var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
        //    if (exists)
        //        throw new BadRequestException("نام کاربری تکراری است");

        //    var passwordHash = SecurityHelper.GetSha256Hash(password);
        //    user.PasswordHash = passwordHash;
        //    await base.AddAsync(user, cancellationToken);
        //}
    }
}
