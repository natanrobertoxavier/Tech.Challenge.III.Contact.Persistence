using Contact.Persistence.Communication.Request;
using Contact.Persistence.Application.Services.LoggedUser;
using Contact.Persistence.Domain.Repositories;
using Contact.Persistence.Domain.Repositories.Contact;
using Contact.Persistence.Domain.Repositories.DDD;
using Contact.Persistence.Exceptions;
using Contact.Persistence.Exceptions.ExceptionBase;
using Contact.Persistence.Communication.Response;

namespace Contact.Persistence.Application.UseCase.Contact.Register;
public class RegisterContactUseCase(
    IContactReadOnlyRepository contactReadOnlyRepository,
    IRegionDDDReadOnlyRepository regionDDDReadOnlyRepository,
    IContactWriteOnlyRepository contactWriteOnlyRepository,
    IMapper mapper,
    IWorkUnit workUnit,
    ILoggedUser loggedUser) : IRegisterContactUseCase
{
    private readonly IContactReadOnlyRepository _contactReadOnlyRepository = contactReadOnlyRepository;
    private readonly IRegionDDDReadOnlyRepository _regionDDDReadOnlyRepository = regionDDDReadOnlyRepository;
    private readonly IContactWriteOnlyRepository _contactWriteOnlyRepository = contactWriteOnlyRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private Guid GuidNull = Guid.Empty;

    public async Task<Result<MessageResult>> RegisterContactAsync(RequestContactJson request)
    {
        var dddId = await Validate(request);

        var loggedUser = await _loggedUser.RecoverUser();

        var entity = _mapper.Map<Domain.Entities.Contact>(request);
        entity.DDDId = dddId;
        entity.UserId = loggedUser.Id;

        await _contactWriteOnlyRepository.Add(entity);

        await _workUnit.Commit();

        throw new NotImplementedException();
    }

    private async Task<Guid> Validate(RequestContactJson request)
    {
        var validator = new RegisterContactValidator();
        var validationResult = validator.Validate(request);

        var regionDDD = await _regionDDDReadOnlyRepository.RecoverListByDDDAsync(request.DDD);

        if (regionDDD is null || !regionDDD.Any())
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("ddd",
                ErrorsMessages.DDDNotFound));

        var thereIsContact = await _contactReadOnlyRepository.ThereIsRegisteredContact(
            regionDDD is not null ? regionDDD.Select(c => c.Id).FirstOrDefault() : GuidNull,
            request.PhoneNumber);

        if (thereIsContact)
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("contact",
                ErrorsMessages.ContactAlreadyRegistered));

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }

        return regionDDD.Select(c => c.Id).FirstOrDefault();
    }
}
