using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.UsersService;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
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
    public async Task<IActionResult> CreateUser()
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> EditUser(int userId)
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        return Ok();
    }
}