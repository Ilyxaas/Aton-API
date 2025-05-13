using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Services;

public class PasswordService : IPasswordService
{
    public string GetHash(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            StringBuilder builder = new StringBuilder();
            
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            
            return builder.ToString();
        }
    }
    
    
}