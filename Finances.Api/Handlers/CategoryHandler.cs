using Finances.Api.Data;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Categories;
using Finances.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finances.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category()
                {
                    Title = request.Title,
                    Description = request.Description,
                    UserId = request.UserId,
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria criado com sucesso!");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possivel criar uma categoria!");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não Encontrada!"); 

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria removida com sucesso!"); 
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possivel criar uma categoria!");
            }
        }

        public async Task<PagedResponse<List<Category?>>> GetAllAsync(GetAllCategoryRequest request)
        {
            try
            {
                var query =  context
                                  .Categories
                                  .AsNoTracking()
                                  .Where(x=>x.UserId ==request.UserId);

                var categories = await query
                                        .Skip((request.PageNumber-1) * request.PageSize)
                                        .Take(request.PageSize)
                                        .ToListAsync();

                var count = query.Count();

                if (!categories.Any())
                    return new PagedResponse<List<Category?>>(null, 404, "nunhuma categoria encontrada!");

                return new PagedResponse<List<Category?>>(categories, count, request.PageNumber, request.PageSize); 
                
            }
            catch
            {
                return new PagedResponse<List<Category?>>(null, 500, null);
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context
                    .Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category == null)
                    return new Response<Category?>(null, 404, "categoria não encontrada");

                return new Response<Category?>(category, message: "Categoraia encontrada!"); 
            }
            catch
            {
                return new Response<Category?>(null, 500, "Listar a categoria!");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context
                             .Categories
                             .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "categoria não encontrada");

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(data: category, message: "categoria actualizado com sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possivel Actualizar a categoria!");
            }
        }
    }
}