using Finances.Api.Common.Api;
using Finances.Core;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Categories;
using Finances.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finances.Api.Endpoints.Categories; 

public class GetAllCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandlerAsync)
            .WithName("Category: GetAll")
            .WithOrder(4)
            .Produces<PagedResponse<List<Category?>>>(); 
    }
    private static async Task<IResult> HandlerAsync(ICategoryHandler handler,
        [FromQuery] int pageSize = Configuration.DefaultPageSize,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber)
    {
        var request = new GetAllCategoryRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserId = ApiConfiguration.UserId
        }; 

        var result = await handler.GetAllAsync(request);

        return result.IsSucess ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }  
}
