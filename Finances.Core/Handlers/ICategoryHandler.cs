using Finances.Core.Models;
using Finances.Core.Requests.Categories;
using Finances.Core.Responses;

namespace Finances.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
        Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category?>>> GetAllAsync(GetAllCategoryRequest request);
          
    }
}
