using Finances.Api.Common.Api;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Categories;
using Finances.Core.Responses;

namespace Finances.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandlerAsync)
            .WithName("Category : Create")
            .WithOrder(1)
            .Produces<Response<Category?>>();
    }

    private static  async Task<IResult> HandlerAsync( 
        ICategoryHandler handler, 
        CreateCategoryRequest request)
    {
        request.UserId = ApiConfiguration.UserId; 

        var result = await handler.CreateAsync(request);

        return result.IsSucess ? 
            TypedResults.Created($"v1/create/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result); 
    }
}
