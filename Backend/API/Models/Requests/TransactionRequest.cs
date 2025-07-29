namespace Person.Models.Requests
{
    public record TransactionRequest(string Description , string Status, decimal Value, Guid PersonId, DateTime Date);

    public record TransactionUpdateRequest(string Description, string Status, decimal Value, DateTime? Date);

}
