﻿using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

namespace PersonTransation.Services
{
    public class TransationService : IService<TransationModel>
    {
        
        public async Task<IResult> Create(TransationModel transation, PersonTransationContext context)
        {
            context.Add(transation);
            await context.SaveChangesAsync();
            return TypedResults.Created();
        }
        public async Task<IResult> Get(PersonTransationContext context, int pageNumber, int pageQuantity)
        {//verificar para exbibir por pagina
            var transation = await context.Transation.ToListAsync();

            var pagination = transation.Skip((pageNumber - 1) * pageQuantity).Take(pageQuantity).ToList();

            return TypedResults.Ok(pagination);
            
        }
        public async Task<Guid> GetId(TransationModel transation, PersonTransationContext context)
        {
            var hasTransation = await context.Transation.FirstOrDefaultAsync(x => x.Description == transation.Description 
                                                                    && x.Status == transation.Status 
                                                                    && x.Value == transation.Value
                                                                    && x.Date == transation.Date);

            if (hasTransation == null)
                return Guid.Empty;
            else
            {
                return hasTransation.Id;
            }

        }
        public async Task<IResult> Patch(TransationModel transation, PersonTransationContext context,Guid id)
        {
            var hasTransation = await context.Transation.FirstOrDefaultAsync(x => x.Id == id);
            
            if(hasTransation == null) 
                return TypedResults.BadRequest("Invalid Id");
            else
            {
                hasTransation.ChangeAttributes(transation.Description, transation.Value, transation.Date);
                await context.SaveChangesAsync();
                return TypedResults.Ok(hasTransation);
            }
        }

        public async Task<IResult> Delete(PersonTransationContext context, Guid id)
        {
            var hasTransation = await context.Transation.FirstOrDefaultAsync(x => x.Id == id);

            if (hasTransation == null) 
                return TypedResults.BadRequest("Invalid Id");
            else
            {
                context.Remove(hasTransation);
                await context.SaveChangesAsync();
                return TypedResults.Ok(hasTransation);
            }
        }

         

    }
}
