using Microsoft.EntityFrameworkCore;

namespace MPN.Data;

public class SearchVendorPartNumbersByPartNumber : IRequest<Result<List<VendorPartNumber>>>
{
    public required string PartNumber { get; set; }
}

internal class SearchVendorPartNumbersByPartNumberHandler(AppDbContext dbContext, ILogger<SearchVendorPartNumbersByPartNumberHandler> logger) : IRequestHandler<SearchVendorPartNumbersByPartNumber, Result<List<VendorPartNumber>>>
{
    public async Task<Result<List<VendorPartNumber>>> Handle(SearchVendorPartNumbersByPartNumber request, CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext.VendorPartNumbers
                .Include(p => p.Item)
                .Include(p => p.Vendor)
                .Where(p => p.PartNumber.StartsWith(request.PartNumber))
                .ProjectToModel()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error handling request: {request}", request);

            return Error.ServerError();
        }
    }
}