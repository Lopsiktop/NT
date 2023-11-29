using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NT.Application.Teachers.Commands.Create;
using NT.Contracts.Teachers;
using NT.Infrastructure.Authentication;

namespace NT.WebApi.Controllers;

[Route("api/Teachers")]
[Authorize(Policy = IdentityPolicy.AdminPolicy)]
public class TeachersController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TeachersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTeacher(TeacherRequest request)
    {
        //var command = _mapper.Map<CreateTeacherCommand>(request);
        var command = new CreateTeacherCommand(request.Name, request.Surname, request.Patronymic, request.UserId, GetJwtToken());
        var result = await _mediator.Send(command);
        return result.Match(Ok, Problem);
    }
}
