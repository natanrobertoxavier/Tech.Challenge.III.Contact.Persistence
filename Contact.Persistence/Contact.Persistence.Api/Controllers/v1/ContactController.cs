using Contact.Persistence.Api.Filters;
using Contact.Persistence.Application.UseCase.Contact.Register;
using Contact.Persistence.Communication.Request;
using Contact.Persistence.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Persistence.Api.Controllers.v1;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class ContactController : TechChallengeController
{
    [HttpPost]
    [ProducesResponseType(typeof(Communication.Response.Result<MessageResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterContact(
        [FromServices] IRegisterContactUseCase useCase,
        [FromBody] RequestContactJson request)
    {
        var result = await useCase.RegisterContactAsync(request);

        return Ok(result);
    }

    //[HttpPut]
    //[ProducesResponseType(typeof(Communication.Response.Result<MessageResult>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> Update(
    //    [FromQuery][Required] Guid id,
    //    [FromBody][Required] RequestContactJson request,
    //    [FromServices] IUpdateContactUseCase useCase)
    //{
    //    await useCase.Execute(id, request);

    //    return NoContent();
    //}

    //[HttpDelete]
    //[ProducesResponseType(typeof(Communication.Response.Result<MessageResult>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> Remove(
    //    [FromQuery][Required] Guid id,
    //    [FromServices] IDeleteContactUseCase useCase)
    //{
    //    var result = await useCase.Execute(id);

    //    if (result)
    //        return NoContent();

    //    return UnprocessableEntity(ErrorsMessages.NoContactsFound);
    //}
}
