using Finances.Api.Common.Api;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Categories;
using Finances.Core.Responses;

namespace Finances.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:long}", HandlerAsync)
            .WithName("Category : Update")
            .WithOrder(2)
            .Produces<Response<Category?>>(); 
    }

    private static async Task<IResult> HandlerAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id; 

        var result = await handler.UpdateAsync(request);

        return result.IsSucess ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result);

    }
}
