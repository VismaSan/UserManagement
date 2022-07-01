namespace UserManagement.Services.UsersService;

public class UsersService : IUsersService
{
    public Task<IEnumerable<int>> GetUsers()
    {
        return Task.FromResult(Enumerable.Range(1, 5));
    }

    public Task<int> GetUser(int id)
    {
        var sequence = Enumerable.Range(1, 6);
        var result = sequence.Contains(id) ? id : 0;

        return Task.FromResult(result);
    }
}