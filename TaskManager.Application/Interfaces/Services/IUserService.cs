using TaskManager.Application.Dtos.User;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services.Base;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Services;

public interface IUserService : IBaseService<UserEntity, IUserRepository, UserResponseDto, UserDto>
{
}
