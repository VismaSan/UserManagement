using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Models;
using UserManagement.Services.UsersService;

namespace UserManagement.Api.Controllers;

// TODO: versioning
// TODO: readme file
// TODO: exception handling
// TODO: create users from list in file
// TODO: edit user

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUsersService usersService, ILogger<UsersController> logger)
    {
        _usersService = usersService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _usersService.GetUsers());
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        return Ok(await _usersService.GetUser(userId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        return Ok(await _usersService.CreateUser(user));
    }

    // [HttpPost]
    // public async Task<IActionResult> CreateUsersFromFile()
    // {
    //     return Ok();
    // }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(int userId, User user)
    {
        return Ok(await _usersService.UpdateUser(userId, user));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        bool removed = await _usersService.DeleteUser(userId);

        return removed ? Ok() : BadRequest();
    }
}