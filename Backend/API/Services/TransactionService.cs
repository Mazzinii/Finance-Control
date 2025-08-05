using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using PersonTransation.Models.DTOs;
using PersonTransation.Models.Entities;

namespace PersonTransation.Services
{
    public class TransactionService : IModel<TransactionModel>
    {

        private readonly IMapper _mapper;

        public TransactionService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IResult> Create(TransactionModel transation, PersonTransactionContext context)
        {
            try
            {
                context.Add(transation);
                await context.SaveChangesAsync();
                return TypedResults.Created();
            }
            catch(Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }
        }
        public async Task<IResult> Get(PersonTransactionContext context, Guid personId, int page, int limit)
        {
            try
            { 
                var transation =   context.Transations.Where(x => x.UserId == personId).AsQueryable();

                var pagination = await transation.Skip((page - 1) * limit).Take(limit).ToListAsync();

                var paginationView = _mapper.Map<List<TransactionDTO>>(pagination);

                return TypedResults.Ok(paginationView);
            }
            catch(Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }
            
        }
        public async Task<IResult> GetId(TransactionModel transation, PersonTransactionContext context)
        {
            try
            {
                var hasTransation = await context.Transations.FirstOrDefaultAsync(x => x.Description == transation.Description
                                                                        && x.Status == transation.Status
                                                                        && x.Value == transation.Value
                                                                        && x.Date == transation.Date);

                if (hasTransation == null)
                    return TypedResults.NotFound("transation not found");
                else
                {
                    return TypedResults.Ok(hasTransation.Id);
                }
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }

        }
        public async Task<IResult> Patch(TransactionModel transation, PersonTransactionContext context,Guid id)
        {
            try
            {
                var hasTransation = await context.Transations.FirstOrDefaultAsync(x => x.Id == id);

                if (hasTransation == null)
                    return TypedResults.BadRequest("Invalid Id");
                else
                {
                    hasTransation.ChangeAttributes(transation.Description, transation.Status, transation.Value, transation.Date);
                    await context.SaveChangesAsync();
                    return TypedResults.Ok(hasTransation);
                }
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }
        }

        public async Task<IResult> Delete(PersonTransactionContext context, Guid id)
        {
            try
            {
                var hasTransation = await context.Transations.FirstOrDefaultAsync(x => x.Id == id);

                if (hasTransation == null)
                    return TypedResults.BadRequest("Invalid Id");
                else
                {
                    context.Remove(hasTransation);
                    await context.SaveChangesAsync();
                    return TypedResults.Ok(hasTransation);
                }
            }
            catch(Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }
        }

         

    }
}
