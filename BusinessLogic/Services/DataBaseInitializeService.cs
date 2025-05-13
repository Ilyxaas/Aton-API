using DataAccess.Models;
using DataAccess.Repository;

namespace BusinessLogic.Services;

public class DataBaseInitializeService(
    IUserRepository repository,
    IPasswordService passwordService) : IDataBaseInitializeService
{
    public static readonly string AdminGuid = "83ba54c1-916a-49f2-9b63-9b8fe156e82a";

    public static readonly string UserGuid = "26f7fdcc-14b9-4166-9c3c-29b733df3ecf";
    
    public async Task InitializeAsync()
    {
        if (await repository.IsEmpty())
        {
            await repository.AddAsync(new User()
            {
                Login = "IlyaAton",
                Admin = true,
                Password = passwordService.GetHash("1234567890"),
                CreatedOn = DateTime.Now.ToUniversalTime(),
                Name = "Ilya",
                CreatedBy = "IlyaAton",
                Gender = 1,
                Guid = new Guid(AdminGuid),
                Birthday = DateTime.Now.ToUniversalTime()
            });
            
            await repository.AddAsync(new User()
            {
                Login = "IlyaAtonUser",
                Admin = true,
                Password = passwordService.GetHash("1234567890"),
                CreatedOn = DateTime.Now.ToUniversalTime(),
                Name = "Nastya",
                CreatedBy = "IlyaAton",
                Gender = 0,
                Guid = new Guid(UserGuid),
                Birthday = null
            });
        }
    }
}