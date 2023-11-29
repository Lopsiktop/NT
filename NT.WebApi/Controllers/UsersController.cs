using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NT.Application.Users.Commands.Register;
using NT.Application.Users.Queries.Login;
using NT.Contracts.Users;

namespace NT.WebApi.Controllers;

[Route("api/Users")]
public class UsersController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRequest request)
    {
        var command = _mapper.Map<UserRegisterCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(Ok, Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserRequest request)
    {
        var query = _mapper.Map<UserLoginQuery>(request);
        var result = await _mediator.Send(query);
        return result.Match(Ok, Problem);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("is auth");
    }
}
