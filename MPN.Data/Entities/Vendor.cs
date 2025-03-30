using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MPN.Data.Entities;

[Index(nameof(Name), IsUnique = true)]
[PrimaryKey(nameof(Id))]
internal class Vendor
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MaxLength(41)]
    public required string Name { get; set; }

    [MaxLength(64)]
    public string? ListID { get; set; }
}
