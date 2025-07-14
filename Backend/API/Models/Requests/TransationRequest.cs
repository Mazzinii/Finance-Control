namespace Person.Models.Requests
{
    public record TransationRequest(string Description , string Status, decimal Value, Guid PersonId, DateTime? Date);

    public record TransationUpdateRequest(string Description, string Status, decimal Value, DateTime? Date);

}
