using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPN.Data;

public class CreateItem : IRequest<Result<Item>>
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(31)]
    public required string Name { get; set; }

    public int? ParentId { get; set; }

    [MaxLength(31)]
    public string? PartNumber { get; set; }

    [MaxLength(4095)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(15,5)")]
    public decimal Cost { get; set; }

    internal string? FullName { get; set; }
}

internal class CreateItemHandler(AppDbContext dbContext, ILogger<CreateItemHandler> logger) : IRequestHandler<CreateItem, Result<Item>>
{
    public async Task<Result<Item>> Handle(CreateItem request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ParentId != null)
            {
                var parentResult = await SetFullName(request, cancellationToken);
                if (!parentResult.WasSuccessful)
                {
                    return Error.NotFound(nameof(request.ParentId));
                }
            }

            var entity = dbContext.Items.Add(new Entities.Item()
            {
                Cost = request.Cost,
                Description = request.Description,
                FullName = request.FullName,
                Name = request.Name,
                ParentId = request.ParentId,
                PartNumber = request.PartNumber
            });

            await dbContext.SaveChangesAsync(cancellationToken);

            return entity.Entity.AsModel();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error handling request: {request}", request);

            return Error.ServerError();
        }
    }

    // TODO: Can we make this function better?
    // Uses recursive querying to get all of the parents for an item, and it's parent's parents, etc.
    private async Task<Result> SetFullName(CreateItem request, CancellationToken cancellationToken)
    {
        // The FullName will always have at least the item name.
        request.FullName = request.Name;

        // No Parent no more additions needed
        if (request.ParentId == null || request.ParentId == 0)
        {
            return Result.Ok();
        }

        // Try to get the parent for the request
        var currentParent = await dbContext.Items.SingleOrDefaultAsync(p => p.Id.Equals(request.ParentId), cancellationToken);
        if (currentParent == null)
        {
            return Error.NotFound();
        }

        // Add the parent to our current name
        request.FullName = $"{currentParent.Name}:{request.FullName}";

        // Keep prepending the names until we don't have any more parents
        while (currentParent.ParentId != null)
        {
            // Get the next parent
            currentParent = await dbContext.Items.SingleOrDefaultAsync(p => p.Id.Equals(currentParent.ParentId), cancellationToken);

            // Ensure we got a parent. This should never not be found if we have a ParentId
            if (currentParent == null)
            {
                return Error.NotFound();
            }

            // Add the current parent to our existing name
            request.FullName = $"{currentParent.Name}:{request.FullName}";
        }

        return Result.Ok();
    }
}
