using Finances.Api.Data;
using Finances.Core.Commom;
using Finances.Core.Handlers;
using Finances.Core.Models;
using Finances.Core.Requests.Transactions;
using Finances.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finances.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                if (request is { Type: Core.Enums.ETransactionType.Withdwaw, Amount: > 0 })
                    request.Amount *= -1;

                var transaction = new Transaction()
                {
                    Title = request.Title,
                    CategoreyId = request.CategoryId,
                    Amount = request.Amount,
                    Type = request.Type,
                    CreateAt = DateTime.UtcNow,
                    PaidOrReceivedAt = request.PaidOrReceived,
                    UserId = request.UserId
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso!");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não possivel criar a transação!");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transacation = await context
                                    .Transactions
                                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transacation is null)
                    return new Response<Transaction?>(null, 404, "Transacção não encontrada!");

                context.Transactions.Remove(transacation);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transacation, message: "Transacção removida com sucesso!");

            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel remover a transação!");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transacation = await context
                                    .Transactions
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transacation is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada!");

                return new Response<Transaction?>(transacation, message: "Transação encontrada!");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel achar a Transacção");
            }
        }

        public async Task<PagedResponse<List<Transaction?>>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();

                var query = context.Transactions
                    .AsNoTracking()
                    .Where(x => x.PaidOrReceivedAt >= request.StartDate
                        && x.PaidOrReceivedAt <= request.EndDate
                        && x.UserId == request.UserId)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query
                                            .AsNoTracking()
                                            .Skip((request.PageNumber - 1) * request.PageSize)
                                            .Take(request.PageSize)
                                            .ToListAsync();

                var count = query.Count();

                return new PagedResponse<List<Transaction?>>(transactions, count, request.PageNumber, request.PageSize);

            }
            catch
            {
                return new PagedResponse<List<Transaction?>>(null, 500, "Não poi possivel listar as Transacções");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                if (request is { Type: Core.Enums.ETransactionType.Withdwaw, Amount: > 0 })
                    request.Amount *= -1;

                var transacation = await context
                                    .Transactions
                                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transacation is null)
                    return new Response<Transaction?>(null, 404, "Transacção não encontrada!");

                transacation.Title = request.Title;
                transacation.CategoreyId = request.CategoryId;
                transacation.Amount = request.Amount;
                transacation.Type = request.Type;
                transacation.PaidOrReceivedAt = request.PaidOrReceived;

                context.Transactions.Update(transacation);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transacation, message: "Transacção actualuzada com sucesso!");

            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel actualizar a transação!");
            }
        }
    }
}
