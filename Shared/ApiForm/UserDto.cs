using System.Text.Json;

namespace Shared.ApiForm;

public class UserDto
{
    public string Login { get; set; }
    
    public string Password { get; set; }

    public string Name { get; set; }

    public int Gender { get; set; }
    
    public DateTime? Birthday { get; set; }
    public bool Active { get; set; }

    public override string ToString()
    {
       return JsonSerializer.Serialize(this);
    }
}