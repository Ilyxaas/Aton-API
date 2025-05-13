using System.ComponentModel.DataAnnotations;

namespace Shared.ApiForm;


public class CreateUserForm
{
    [Required(ErrorMessage = "Login field is Required")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password field is Required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Name field is Required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Gender field is Required")]
    [Range(0,2)]
    public int Gender { get; set; }
    
    public DateTime? Birthday { get; set; }

    [Required (ErrorMessage = "Admin field is Required")]
    public bool Admin { get; set; }
}