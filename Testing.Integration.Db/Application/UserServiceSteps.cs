using Application;
using Domain.Entities;
using FluentAssertions;
using Framework;
using Persistence;

namespace Integration.Db.Application;

public partial class UserServiceShould() : TruncateDbSpecification(Settings.Database.Connection)
{
    private Guid id;
    private Guid another_id;
    private User? retrieved_entity;
    private IList<User> entities = null!;
    private UserRepository repository = null!;
    private UserService userService = null!;

    private const string name = "wibble";
    private const string email = "wobble@gmail.com";
    private const string new_name = "wobble";
    private const string new_email = "wibble@gmail.com";

    protected override void before_each()
    {
        base.before_each();
        id = default;
        another_id = default;
        entities = null!;
        retrieved_entity = null!;
        repository = new UserRepository(new Persistence.Db(Settings.Database.Connection));
        userService = new UserService(repository);
    }
    
    private static void user_details(){}     
    
    private void adding_a_user()
    {
        id = userService.Add(name, email).Await();
    }    
    
    private void saving_another_user()
    {
        another_id = userService.Add(new_name, new_email).Await();
    }
    
    private void a_user_exists()
    {
        adding_a_user();
    }

    private void another_user_exists()
    {
        saving_another_user();
    }    
    
    private void saving_another_user_with_same_email()
    {
        userService.Add(new_name, email).Await();
    }    
    
    private void retrieving_a_user()
    {
        retrieved_entity = userService.Get(id).Await();
    }        
    
    private void listing_entities()
    {
        entities = userService.GetAll().Await();
    }     
    
    private void the_user_is_correct()
    {
        retrieved_entity?.Id.Should().Be(id);
        retrieved_entity?.FullName.ToString().Should().Be(name);
        retrieved_entity?.Email.ToString().Should().Be(email);
    }
    
    private void the_list_is_correct()
    {
        entities.Count.Should().Be(2);
        var user = entities.FirstOrDefault(e => e.Id == id);
        user!.Id.Should().Be(id);
        user.FullName.ToString().Should().Be(name);
        user.Email.ToString().Should().Be(email);
        var anotherUser = entities.FirstOrDefault(e => e.Id == another_id);
        anotherUser!.Id.Should().Be(another_id);
        anotherUser.FullName.ToString().Should().Be(new_name);
        anotherUser.Email.ToString().Should().Be(new_email);
    }
}