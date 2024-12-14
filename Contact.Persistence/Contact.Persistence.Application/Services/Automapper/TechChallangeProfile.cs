using AutoMapper;

namespace Contact.Persistence.Application.Services.Automapper;
public class TechChallengeProfile : Profile
{
    public TechChallengeProfile()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void EntityToResponse()
    {
    }

    private void RequestToEntity()
    {
    }
}
