using System.ComponentModel.DataAnnotations;

namespace MPN.Data;

public class Vendor
{
    public int Id { get; set; }

    [Required()]
    [MaxLength(41)]
    public required string Name { get; set; }

    [MaxLength(64)]
    public string? ListID { get; set; }
}
