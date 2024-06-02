using Finances.Api.Common.Api;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Transactions;
using Finances.Core.Responses;

namespace Finances.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:long}", HandlerAsync)
            .WithName("Transaction : Update")
            .WithOrder(2)
            .Produces<Response<Transaction>>(); 
    }

    private static async Task<IResult> HandlerAsync( ITransactionHandler handler, UpdateTransactionRequest request, long id)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id; 

        var result = await handler.UpdateAsync(request);

        return result.IsSucess ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result); 
    }
}
