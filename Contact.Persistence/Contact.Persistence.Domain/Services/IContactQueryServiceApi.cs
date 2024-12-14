using Contact.Persistence.Domain.ResultServices;

namespace Contact.Persistence.Domain.Services;
public interface IContactQueryServiceApi
{
    //Task<Result<ListContactResult>> RecoverContactsByDDDAsync(int dDD);
    Task<Result<ThereIsContactResult>> ThereIsContactAsync(int ddd, string phoneNumber, string token);
}
