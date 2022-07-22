using Entities.Account;

namespace Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(User user);
    }
}