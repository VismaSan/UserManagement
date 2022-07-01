namespace UserManagement.Services.UsersService;

public interface IUsersService
{
    public Task<IEnumerable<int>> GetUsers();
    public Task<int> GetUser(int id);
}