using Microsoft.EntityFrameworkCore;
using Person.Data;
using PersonTransation.Models.Entities;

namespace PersonTransation.Services
{
    public class TransationService : IModel<TransationModel>
    {
        
        public async Task<IResult> Create(TransationModel transation, PersonTransationContext context)
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
        public async Task<IResult> Get(PersonTransationContext context, Guid personId, int page, int limit)
        {
            try
            { 
                var transation =  await context.Transations.Where(x => x.UserId == personId).ToListAsync();

                var pagination = transation.Skip((page - 1) * limit).Take(limit).ToList();

                return TypedResults.Ok(pagination);
            }
            catch(Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }
            
        }
        public async Task<IResult> GetId(TransationModel transation, PersonTransationContext context)
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
        public async Task<IResult> Patch(TransationModel transation, PersonTransationContext context,Guid id)
        {
            try
            {
                var hasTransation = await context.Transations.FirstOrDefaultAsync(x => x.Id == id);

                if (hasTransation == null)
                    return TypedResults.BadRequest("Invalid Id");
                else
                {
                    hasTransation.ChangeAttributes(transation.Description, transation.Value, transation.Date);
                    await context.SaveChangesAsync();
                    return TypedResults.Ok(hasTransation);
                }
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }
        }

        public async Task<IResult> Delete(PersonTransationContext context, Guid id)
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
