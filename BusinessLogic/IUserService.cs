using DataAccess.Models;
using Shared.ApiForm;

namespace BusinessLogic;

public interface IUserService
{
    Task CreateAsync(CreateUserForm userForm, Guid creatorGuid);
    Task<List<User>> GetActiveUsersAsync(Guid creatorGuid);
    Task<UserDto> GetUserByLoginAsync(Guid creatorGuid, string login);
    Task<List<User>> GetUsersOlderThanAsync(DateTime dateTime, Guid creatorGuid);
    Task<User> GetUserByLoginAndPasswordAsync(string login, string password);
    Task RecoverAsync(Guid creatorGuid, Guid recoverableGuid);
    Task SoftDeleteAsync(Guid creatorGuid, string removeUserLogin);
    Task HardDeleteAsync(Guid creatorGuid, string removeUserLogin);
    Task ChangePasswordAsync(Guid creatorGuid, Guid userChangeGuid, string newPassword);
    Task ChangeLoginAsync(Guid creatorGuid, Guid userChangeGuid, string newLogin);
    Task UpdateUserProfileAsync(Guid creatorGuid, Guid userChangeGuid, UpdateUserDataForm dataForm);
}