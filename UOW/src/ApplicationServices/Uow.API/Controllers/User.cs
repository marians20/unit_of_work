using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uow.Domain.Contracts;
using Uow.Domain.Dtos;

namespace Uow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class User : ControllerBase
{
    private readonly IUserService _service;
    private readonly IHttpContextAccessor _accessor;
    public User(IUserService service, IHttpContextAccessor accessor)
    {
        _service = service;
        _accessor = accessor;
    }

    /// <summary>
    /// Creates a new  user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(UserCreateDto user, CancellationToken cancellationToken) =>
        Ok(await _service.CreateAsync(user, cancellationToken));

    /// <summary>
    /// Updates a user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<Guid>> Update(UserDto user, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(user, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Retrieves all users
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> All(CancellationToken cancellationToken) =>
        Ok(await _service.AllAsync(cancellationToken));

    /// <summary>
    /// Gets user by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("/[controller]/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken) =>
        Ok(await _service.GetByIdAsync(id, cancellationToken));

}