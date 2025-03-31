using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Persistence;

namespace Application;

public interface IAmAUserService
{
    public Task<User?> Get(Guid id);
    public Task<IList<User>> GetAll();
    public Task<Guid> Add(string name, string email);
}

public class UserService(UserRepository repository) : IAmAUserService
{
    public async Task<User?> Get(Guid id)
    {
        var user = await repository.Get(id);
        return user;
    }

    public async Task<IList<User>> GetAll()
    {
        return await repository.GetAll();
    }

    public async Task<Guid> Add(string name, string email)
    {
        var id = Guid.NewGuid();
        if ((await repository.GetAll()).Any(u => u.Email == email))
        {
            throw new ValidationException("User with same email already exists");
        }
        var user = new User(id, name, email);
        await repository.Save(user);
        return id;
    }
}
