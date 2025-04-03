using Person.Data;

namespace PersonTransation.Services
{
    public interface IModel<T>
    {
        
        Task<IResult> Create(T model, PersonTransationContext context);
        Task<IResult> Get(PersonTransationContext context, int page, int limit);
        Task<IResult> Patch(T model, PersonTransationContext context, Guid id);
        Task<IResult> Delete(PersonTransationContext context, Guid id);
    }
}