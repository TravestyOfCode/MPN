using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPN.Data.Entities;

[PrimaryKey(nameof(Id))]
[Index(nameof(PartNumber), IsUnique = false)]
[Index(nameof(ItemId), nameof(VendorId), nameof(PartNumber), IsUnique = true)]
internal class VendorPartNumber
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public Item? Item { get; set; }

    public int VendorId { get; set; }

    public Vendor? Vendor { get; set; }

    [Required]
    public required string PartNumber { get; set; }

    [MaxLength(4095)]
    public string? VendorDescription { get; set; }

    [Column(TypeName = "decimal(15,5)")]
    public decimal? VendorCost { get; set; }
}
