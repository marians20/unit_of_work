using Microsoft.AspNetCore.Mvc;
using Uow.PrimaryPorts;
using Uow.PrimaryPorts.Dtos;

namespace Uow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class Role : ControllerBase
{
    private readonly IRoleService service;

    public Role(IRoleService service) => this.service = service;

    /// <summary>
    /// Creates a new role
    /// </summary>
    /// <param name="role"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(RoleCreateDto role, CancellationToken cancellationToken) =>
        Ok(await service.CreateAsync(role, cancellationToken));

    /// <summary>
    /// Updates a role
    /// </summary>
    /// <param name="role"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<Guid>> Update(RoleDto role, CancellationToken cancellationToken)
    {
        await service.UpdateAsync(role, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Deletes a role
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Retrieves all roles
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RoleDto>>> All(CancellationToken cancellationToken) =>
        Ok(await service.AllAsync(cancellationToken));

    /// <summary>
    /// Gets role by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("/[controller]/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken) =>
        Ok(await service.GetByIdAsync(id, cancellationToken));

}