using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPN.Data;

public class VendorPartNumber
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public required string ItemFullName { get; set; }

    public string? ItemPartNumber { get; set; }

    public int VendorId { get; set; }

    public required string VendorName { get; set; }

    [Required]
    public required string PartNumber { get; set; }

    [MaxLength(4095)]
    public string? VendorDescription { get; set; }

    [Column(TypeName = "decimal(15,5)")]
    public decimal? VendorCost { get; set; }
}

internal static class VendorPartNumberExtensions
{
    public static IQueryable<VendorPartNumber> ProjectToModel(this IQueryable<Entities.VendorPartNumber> source) =>
        source.Select(p => new VendorPartNumber()
        {
            Id = p.Id,
            ItemId = p.ItemId,
            ItemFullName = p.Item!.FullName!, // TODO: Change Item.FullName is not null as we don't allow it on create or update?
            ItemPartNumber = p.Item.PartNumber,
            VendorId = p.VendorId,
            VendorName = p.Vendor!.Name,
            PartNumber = p.PartNumber,
            VendorDescription = p.VendorDescription,
            VendorCost = p.VendorCost
        });
}