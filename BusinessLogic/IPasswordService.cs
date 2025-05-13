namespace BusinessLogic;

public interface IPasswordService
{
    public string GetHash(string password);
}