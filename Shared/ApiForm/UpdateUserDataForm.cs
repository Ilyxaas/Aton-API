using System.ComponentModel.DataAnnotations;

namespace Shared.ApiForm;

public class UpdateUserDataForm
{
    public string? Name { get; set; }

    [Range(0,2)]
    public int? Gender { get; set; }
    
    public DateTime? Birthday { get; set; }
}