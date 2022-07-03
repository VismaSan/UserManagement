using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Models;
using UserManagement.Services.UsersService;

namespace UserManagement.Api.Controllers;

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

    [HttpPost("upload")]
    public async Task<IActionResult> UploadUsers(IFormFile file)
    {
        string fileExtension = Path.GetExtension(file.FileName);

        if (fileExtension != ".csv")
        {
            return BadRequest("Only .csv file extensions are supported.");
        }

        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        string filePath = Path.Combine(directoryPath, file.FileName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        await using (FileStream fileStream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(fileStream);
        }

        return Ok(await _usersService.UploadUsers(filePath));
    }

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