namespace Person.Models.Requests
{
    public record TransationRequest(string Description , string Status, int Value, DateTime Date, Guid PersonId);

    public record TransationUpdateRequest(string Description, int Value, DateTime Date);

}
