using Finances.Api.Common.Api;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Transactions;
using Finances.Core.Responses;

namespace Finances.Api.Endpoints.Transactions; 

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:long}", HandlerAsync)
            .WithName("Transaction: Delete")
            .WithOrder(3)
            .Produces<Response<Transaction>>(); 
    }
    
    private static async Task<IResult> HandlerAsync(ITransactionHandler handler, long id)
    {
        var request = new DeleteTransactionRequest()
        {
            Id = id,
            UserId = ApiConfiguration.UserId,
        }; 

        var result = await handler.DeleteAsync(request);

        return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);  
    }
}
