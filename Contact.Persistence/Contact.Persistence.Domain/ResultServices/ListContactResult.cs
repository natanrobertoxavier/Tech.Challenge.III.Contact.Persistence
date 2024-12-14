using System.Text.Json.Serialization;

namespace Contact.Persistence.Domain.ResultServices;
public class ListContactResult
{
    [JsonPropertyName("contacts")]
    public IEnumerable<ContactResult> Contacts { get; set; }
}
