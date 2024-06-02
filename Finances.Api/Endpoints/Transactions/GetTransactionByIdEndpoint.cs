using Finances.Api.Common.Api;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Transactions;
using Finances.Core.Responses;

namespace Finances.Api.Endpoints.Transactions; 

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}", HandlerAsync)
            .WithName("Transaction : Get")
            .WithOrder(4)
            .Produces<Response<Transaction?>>(); 
    }

    private static async Task<IResult> HandlerAsync(ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest()
        {
            Id = id,
            UserId = ApiConfiguration.UserId
        };

        var result = await handler.GetByIdAsync(request); 

        return result.IsSucess? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}
