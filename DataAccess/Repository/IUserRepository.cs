using DataAccess.Models;

namespace DataAccess.Repository;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(User user, CancellationToken cancellationToken = default, bool checkLogin = true);
    
    Task<List<User>> GetActiveUsersAsync(CancellationToken cancellationToken = default);
    
    Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default);

    Task<List<User>> GetUsersOlderThanAsync(DateTime date, CancellationToken cancellationToken = default);

    Task<User> GetUserByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

    Task DeleteAsync(User user, CancellationToken cancellationToken = default);

    Task<bool> IsExistLoginAsync(string login);

    Task<bool> IsEmpty();
}