using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPN.Data;

public class VendorPartNumber
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int VendorId { get; set; }

    [Required]
    public required string PartNumber { get; set; }

    [MaxLength(4095)]
    public string? VendorDescription { get; set; }

    [Column(TypeName = "decimal(15,5)")]
    public decimal? VendorCost { get; set; }
}
