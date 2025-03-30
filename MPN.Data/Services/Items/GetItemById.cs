using Microsoft.EntityFrameworkCore;

namespace MPN.Data;

public class GetItemById : IRequest<Result<Item>>
{
    public int Id { get; set; }
}

internal class GetItemByIdHandler(AppDbContext dbContext, ILogger<GetItemByIdHandler> logger) : IRequestHandler<GetItemById, Result<Item>>
{
    public async Task<Result<Item>> Handle(GetItemById request, CancellationToken cancellationToken)
    {
        try
        {
            var item = await dbContext.Items
                .Where(p => p.Id.Equals(request.Id))
                .Select(p => new Item()
                {
                    Id = p.Id,
                    Cost = p.Cost,
                    Description = p.Description,
                    FullName = p.FullName,
                    Name = p.Name,
                    ParentId = p.ParentId,
                    PartNumber = p.PartNumber
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (item == null)
            {
                return Error.NotFound(nameof(request.Id));
            }

            return item;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error handling request: {request}", request);

            return Error.ServerError();
        }
    }
}