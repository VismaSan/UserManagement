using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using KellermanSoftware.CompareNetObjects;
using Moq;
using NUnit.Framework;
using UserManagement.Repositories.Entities;
using UserManagement.Repositories.UsersRepository;
using UserManagement.Services.MapperExtensions;
using UserManagement.Services.Models;
using UserManagement.Services.UsersService;

namespace UserManagement.ServicesTests;

public class UsersServiceTests
{
    private readonly IFixture _fixture;
    private readonly IUsersService _usersService;
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly CompareLogic _compareLogic;

    public UsersServiceTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _usersRepositoryMock = _fixture.Freeze<Mock<IUsersRepository>>();
        _usersService = _fixture.Create<UsersService>();
        _compareLogic = new CompareLogic();
    }

    [Test]
    public async Task Should_return_correct_users_list()
    {
        // Arrange
        var repositoryUsers = _fixture.CreateMany<UserEntity>().ToList();
        var expectedUsers = repositoryUsers.Select(entity => entity.ToModel());

        _usersRepositoryMock
            .Setup(repository => repository.GetUsers())
            .ReturnsAsync(repositoryUsers);

        // Act
        IEnumerable<User> actualUsers = await _usersService.GetUsers();

        // Assert
        _usersRepositoryMock.Verify(x => x.GetUsers(), Times.Once);
        _usersRepositoryMock.VerifyNoOtherCalls();
        ComparisonResult result = _compareLogic.Compare(expectedUsers, actualUsers);
        Assert.IsTrue(result.AreEqual, result.DifferencesString);
    }
}