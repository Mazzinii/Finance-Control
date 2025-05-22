namespace Person.Models.Requests
{
    public record TransationRequest(string Description , string Status, int Value, Guid PersonId, DateTime? Date);

    public record TransationUpdateRequest(string Description, string Status, int Value, DateTime? Date);

}
