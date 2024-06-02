using Finances.Api.Common.Api;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Categories;
using Finances.Core.Responses;

namespace Finances.Api.Endpoints.Categories
{
    public class DelegeCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id:long}", HandlerAsync)
                .WithName("Category : Delete")
                .WithOrder(3)
                .Produces<Response<Category?>>();
        }

        private static async Task<IResult> HandlerAsync(ICategoryHandler handler,
            long id)
        {
            var request = new DeleteCategoryRequest
            {
                Id = id,
                UserId = ApiConfiguration.UserId
            };
            var result = await handler.DeleteAsync(request);

            return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
