using System.Text.RegularExpressions;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;
using Shared.ApiForm;
using Shared.Exceptions;

namespace BusinessLogic.Services;

public sealed class UserService(
    IUserRepository repository,
    IPasswordService passwordService,
    IMapper mapper) : IUserService
{
    private const string Pattern = "^[a-zA-Z0-9]*$";
    
    public async Task CreateAsync(CreateUserForm userForm, Guid creatorGuid)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);

        if (!userCreator.Admin)
            throw new DontHavePermissionException();

        if (!IsLatinAndDigits(userForm.Name))
            throw new ValidationException(ValidationException.GenerateExcMessage(nameof(userForm.Name)));
        
        if (!IsLatinAndDigits(userForm.Login))
            throw new ValidationException(ValidationException.GenerateExcMessage(nameof(userForm.Login)));
        
        if (!IsLatinAndDigits(userForm.Password))
            throw new ValidationException(ValidationException.GenerateExcMessage(nameof(userForm.Password)));
        
        var newUser = new User()
        {
            Login = userForm.Login,
            Admin = userForm.Admin,
            Password = passwordService.GetHash(userForm.Password),
            CreatedOn = DateTime.Now.ToUniversalTime(),
            Name = userForm.Name,
            CreatedBy = userCreator.Login,
            Gender = userForm.Gender,
            Guid = new Guid(),
            Birthday = userForm.Birthday
        };

        await repository.AddAsync(newUser);
    }
        
    //5
    public async Task<List<User>> GetActiveUsersAsync(Guid creatorGuid)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);

        if (!userCreator.Admin)
            throw new DontHavePermissionException();

        return await repository.GetActiveUsersAsync();
    }
    
    //6
    
    public async Task<UserDto> GetUserByLoginAsync(Guid creatorGuid, string login)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);

        var returnUser = await repository.GetUserByLoginAsync(login);
        
        if (!userCreator.Admin)
            throw new DontHavePermissionException();

        if (returnUser is null)
            throw new UserNotFoundException();
        
        return mapper.Map<UserDto>(returnUser);
    }
    
    //8
    public async Task<List<User>> GetUsersOlderThanAsync(DateTime dateTime, Guid creatorGuid)
    {
        dateTime = dateTime.ToUniversalTime();
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);
        
        if (!userCreator.Admin)
            throw new DontHavePermissionException();

        return await repository.GetUsersOlderThanAsync(dateTime);
    }

    //7
    public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password)
    {
        var user = await repository.GetUserByLoginAsync(login);

        if (user.Password != passwordService.GetHash(password))
            throw new PasswordNotCorrectException();
                
        return user;
    }

    //10
    public async Task RecoverAsync(Guid creatorGuid, Guid recoverableGuid)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);

        var recoverUser = await repository.GetUserByGuidAsync(recoverableGuid);
        
        if (!userCreator.Admin)
            throw new DontHavePermissionException();

        recoverUser.RevokedOn = null;
        recoverUser.RevokedBy = null;
        recoverUser.ModifiedOn = DateTime.Now.ToUniversalTime();;
        recoverUser.ModifiedBy = userCreator.Login;

        await repository.UpdateAsync(recoverUser, checkLogin: false);

    }
    
    //9.1
    public async Task SoftDeleteAsync(Guid creatorGuid, string removeUserLogin)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);
        
        var removeUser = await repository.GetUserByLoginAsync(removeUserLogin);
        
        if (!userCreator.Admin)
            throw new DontHavePermissionException();

        removeUser.RevokedOn = DateTime.Now.ToUniversalTime();;
        removeUser.RevokedBy = userCreator.Login;

        await repository.UpdateAsync(removeUser, checkLogin: false);
    }

    //9.2
    public async Task HardDeleteAsync(Guid creatorGuid, string removeUserLogin)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);

        var removeUser = await repository.GetUserByLoginAsync(removeUserLogin);
        
        if (!userCreator.Admin)
            throw new DontHavePermissionException();

        removeUser.RevokedOn = DateTime.Now.ToUniversalTime();
        removeUser.RevokedBy = userCreator.Login;

        await repository.DeleteAsync(removeUser);
    }
    
    //3
    public async Task ChangePasswordAsync(Guid creatorGuid, Guid userChangeGuid, string newPassword)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);

        var userChange = await repository.GetUserByGuidAsync(userChangeGuid);
        
        if (userCreator.Admin == false)
            if (creatorGuid != userChangeGuid && userChange.RevokedOn is null)
                throw new DontHavePermissionException();

        userChange.Password = passwordService.GetHash(newPassword);
        userChange.ModifiedBy = userCreator.Login;
        userChange.ModifiedOn = DateTime.Now.ToUniversalTime();

        await repository.UpdateAsync(userChange);
    }
    
    //4
    public async Task ChangeLoginAsync(Guid creatorGuid, Guid userChangeGuid, string newLogin)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);

        var userChange = await repository.GetUserByGuidAsync(userChangeGuid);
        
        if (userCreator.Admin == false)
            if (creatorGuid != userChangeGuid && userChange.RevokedOn is null)
                throw new DontHavePermissionException();

        if (await repository.IsExistLoginAsync(newLogin))
            throw new LoginExistException();

        userChange.Login = newLogin;
        
        userChange.ModifiedBy = userCreator.Login;
        userChange.ModifiedOn = DateTime.Now.ToUniversalTime();
        
        await repository.UpdateAsync(userChange);
    }
    
    //2
    public async Task UpdateUserProfileAsync(Guid creatorGuid, Guid userChangeGuid, UpdateUserDataForm dataForm)
    {
        var userCreator = await repository.GetUserByGuidAsync(creatorGuid);

        var userChange = await repository.GetUserByGuidAsync(userChangeGuid);

        if (userCreator.Admin == false)
            if (creatorGuid != userChangeGuid && userChange.RevokedOn is null)
                throw new DontHavePermissionException();
        
        userChange.Birthday = dataForm.Birthday ?? userChange.Birthday;

        userChange.Name = dataForm.Name ?? userChange.Name;

        userChange.Gender = dataForm.Gender ?? userChange.Gender;
        
        userChange.ModifiedBy = userCreator.Login;
        userChange.ModifiedOn = DateTime.Now.ToUniversalTime();

        await repository.UpdateAsync(userChange, checkLogin: false);
    }
    
    private bool IsLatinAndDigits(string password)
    {
        return Regex.IsMatch(password, Pattern);
    }

}