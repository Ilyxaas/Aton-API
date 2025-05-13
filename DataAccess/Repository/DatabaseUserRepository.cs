using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace DataAccess.Repository;

public class DatabaseUserRepository(AppContext context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
        
        TimeConvert(user);
        if (await IsExistLoginAsync(user.Login))
            throw new LoginExistException();
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default, bool checkLogin = true)
    {
        context.Users.Update(user);
        if (checkLogin && await IsExistLoginAsync(user.Login))
            throw new LoginExistException();
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<User>> GetActiveUsersAsync(CancellationToken cancellationToken = default)
    {
       var returnObject = await context.Users
           .Where(user => user.RevokedBy == null)
           .OrderBy(user => user.CreatedOn)
           .ToListAsync(cancellationToken);
       
       return returnObject;
    }

    public async Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(user => user.Login == login, cancellationToken);

        if (user == null)
            throw new UserNotFoundException();

        return user;
    }

    public async Task<List<User>> GetUsersOlderThanAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .Where(user => user.Birthday != null && user.Birthday < date)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetUserByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(user => user.Guid == guid, cancellationToken);

        if (user == null)
            throw new UserNotFoundException();

        return user;
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Remove(user);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistLoginAsync(string login)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Login == login);

        return user is not null;
    }
    
    public async Task<bool> IsEmpty()
    {
        return await context.Users.AnyAsync() == false;
    }

    private void TimeConvert(User user)
    {
        if (user.Birthday is not null)
            user.Birthday = user.Birthday.Value.ToUniversalTime();

        user.CreatedOn = user.CreatedOn.ToUniversalTime();
        
        if (user.RevokedOn is not null)
            user.RevokedOn = user.RevokedOn.Value.ToUniversalTime();

        if (user.ModifiedOn is not null)
            user.ModifiedOn = user.ModifiedOn.Value.ToUniversalTime();
    }

    

}