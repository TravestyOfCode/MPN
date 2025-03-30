using Microsoft.EntityFrameworkCore;

namespace MPN.Data;

public class DeleteItem : IRequest<Result>
{
    public int Id { get; set; }
}

internal class DeleteItemHandler(AppDbContext dbContext, ILogger<DeleteItemHandler> logger) : IRequestHandler<DeleteItem, Result>
{
    public async Task<Result> Handle(DeleteItem request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await dbContext.Items.SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Error.NotFound();
            }

            dbContext.Items.Remove(entity);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error handling request: {request}", request);

            return Error.ServerError();
        }
    }
}