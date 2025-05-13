using DataAccess.Models;

namespace DataAccess;

public class LocalUserStorage
{
    public List<User> Users { get; set; } = new();

}