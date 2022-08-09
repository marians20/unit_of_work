// <copyright file="User.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
using Microsoft.AspNetCore.Mvc;
using Uow.PrimaryPorts;
using Uow.PrimaryPorts.Dtos;

namespace Uow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class User : ControllerBase
{
    private readonly IUserService service;

    public User(IUserService service) => this.service = service;

    /// <summary>
    /// Creates a new  user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(UserCreateDto user, CancellationToken cancellationToken) =>
        Ok(await service.CreateAsync(user, cancellationToken));

    /// <summary>
    /// Updates a user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<Guid>> Update(UserDto user, CancellationToken cancellationToken)
    {
        await service.UpdateAsync(user, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Deletes an user
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
    /// Retrieves all users
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> All(CancellationToken cancellationToken) =>
        Ok(await service.AllAsync(cancellationToken));

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
        Ok(await service.GetByIdAsync(id, cancellationToken));

    /// <summary>
    /// Assign an existing role to an existing user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roleId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{userId:guid}/roles/{roleId:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AssignRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        await service.AssignRole(userId, roleId, cancellationToken).ConfigureAwait(false);
        return Ok();
    }

    /// <summary>
    /// Revokes an existing role from an existing user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roleId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{userId:guid}/roles/{roleId:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RevokeRole(Guid userId, Guid roleId, CancellationToken cancellationToken)
    {
        await service.RevokeRole(userId, roleId, cancellationToken).ConfigureAwait(false);
        return Ok();
    }

}