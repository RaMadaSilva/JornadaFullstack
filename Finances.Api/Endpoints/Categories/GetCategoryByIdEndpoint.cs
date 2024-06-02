
using Finances.Api.Common.Api;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Categories;
using Finances.Core.Responses;

namespace Finances.Api.Endpoints.Categories; 

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}", HandlerAsync)
            .WithName("Category: GetById")
            .WithOrder(5)
            .Produces<Response<Category?>>(); 
    }

    private static async Task<IResult> HandlerAsync(ICategoryHandler handler, long id)
    {
        var request = new GetCategoryByIdRequest()
        {
            Id = id,
            UserId = ApiConfiguration.UserId
        };

        var result = await handler.GetByIdAsync(request);

        return result.IsSucess ? TypedResults.Ok(request)
            : TypedResults.BadRequest(request);
    }
}
