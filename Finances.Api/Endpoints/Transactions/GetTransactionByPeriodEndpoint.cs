using Finances.Api.Common.Api;
using Finances.Core;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Transactions;
using Finances.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finances.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandlerAsync)
            .WithName("Transaction: GetbyPeriod")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>>>(); 
    }

    private static async Task<IResult> HandlerAsync(ITransactionHandler handler, 
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime?  endDate = null)
    {
        var request = new GetTransactionByPeriodRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate, 
            UserId = ApiConfiguration.UserId
        }; 

        var result = await handler.GetByPeriodAsync(request);

        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}
