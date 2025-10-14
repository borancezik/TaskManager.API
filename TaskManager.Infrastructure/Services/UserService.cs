using TaskManager.Application.Dtos.User;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Services.Base;

namespace TaskManager.Infrastructure.Services;

public class UserService : BaseService<UserEntity, IUserRepository, UserResponseDto, UserDto>, IUserService
{
    public UserService(IUserRepository repositoryBase) : base(repositoryBase)
    {
    }
}
