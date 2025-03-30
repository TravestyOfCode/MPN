using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPN.Data.Entities;

[PrimaryKey(nameof(Id))]
[Index(nameof(Name), IsUnique = false)]
[Index(nameof(PartNumber), IsUnique = false)]
[Index(nameof(Name), nameof(ParentId), IsUnique = true)]
internal class Item
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MaxLength(31)]
    public required string Name { get; set; }

    public int? ParentId { get; set; }

    public Item? Parent { get; set; }

    [MaxLength(31)]
    public string? PartNumber { get; set; }

    [MaxLength(4095)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(15,5)")]
    public decimal Cost { get; set; }
}
