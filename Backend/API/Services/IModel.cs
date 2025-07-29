using Person.Data;

namespace PersonTransation.Services
{
    public interface IModel<T>
    {

        Task<IResult> Create(T model, PersonTransactionContext context); 
        Task<IResult> Patch(T model, PersonTransactionContext context, Guid id);
        Task<IResult> Delete(PersonTransactionContext context, Guid id);
    }
}