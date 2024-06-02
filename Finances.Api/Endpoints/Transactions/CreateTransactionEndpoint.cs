using Finances.Api.Common.Api;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Transactions;
using Finances.Core.Responses;

namespace Finances.Api.Endpoints.Transactions; 

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandlerAsync)
            .WithName("Transaction : Create")
            .WithOrder(1)
            .Produces<Response<Transaction>>(); 
    }

    private static async Task<IResult> HandlerAsync(ITransactionHandler handler, CreateTransactionRequest request)
    {
        request.UserId = ApiConfiguration.UserId; 
        var result = await handler.CreateAsync(request);

        return result.IsSucess ? TypedResults.Created($"v1/api/{result.Data?.Id}", result)
        : TypedResults.BadRequest(result); 
    }
}
