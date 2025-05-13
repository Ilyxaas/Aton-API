using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DataAccess.Models;

[Table("User",Schema = "public")]
public class User
{
   public Guid Guid { set; get; }

   public string Login { set; get; }

   public string Password { set; get; }

   public string Name { set; get; }

   public int Gender { set; get; }

   public DateTime? Birthday { set; get; }

   public bool Admin { set; get; }

   public DateTime CreatedOn { set; get; }

   public string CreatedBy { set; get; }

   public DateTime? ModifiedOn { set; get; }

   public string? ModifiedBy { set; get; }

   public DateTime? RevokedOn { set; get; }

   public string? RevokedBy { set; get; }
   
   public override string ToString()
   {
      return JsonSerializer.Serialize(this);
   }
}