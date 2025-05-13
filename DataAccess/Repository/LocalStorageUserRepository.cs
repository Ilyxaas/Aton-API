using DataAccess.Models;
using Shared.Exceptions;

namespace DataAccess.Repository;

public class LocalStorageUserRepository(LocalUserStorage context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        if (await IsExistLoginAsync(user.Login))
            throw new LoginExistException();
        
        context.Users.Add(user);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default,bool checkLogin = true)
    {
        var userId = context.Users.FindIndex(x => x.Guid == user.Guid);
        
        if (checkLogin && await IsExistLoginAsync(user.Login))
            throw new LoginExistException();
        
        context.Users[userId] = user; 
    }

    public Task<List<User>> GetActiveUsersAsync(CancellationToken cancellationToken = default)
    {
        var returnObject = context.Users
            .Where(user => user.RevokedBy == null)
            .OrderBy(user => user.CreatedOn)
            .ToList();
        return Task.FromResult(returnObject);
    }

    public Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        var user = context.Users
            .Find(user => user.Login == login);

        if (user == null)
            throw new UserNotFoundException();

        return Task.FromResult(user);
    }

    public Task<List<User>> GetUsersOlderThanAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        var users = context.Users
            .Where(user => user.Birthday != null && user.Birthday < date)
            .ToList();
        return Task.FromResult(users);
    }

    public Task<User> GetUserByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var user = context.Users.Find(x => x.Guid == guid);

        if (user == null)
            throw new UserNotFoundException();

        return Task.FromResult(user);
    }

    public Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Remove(user);

        return Task.CompletedTask;
    }

    public async Task<bool> IsEmpty()
    {
        return await Task.FromResult(context.Users.Any() == false);
    }

    public Task<bool> IsExistLoginAsync(string login)
    {
       var user = context.Users.Find(x => x.Login == login);

       return Task.FromResult(user is not null);
    }
}