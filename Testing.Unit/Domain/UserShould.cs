using NUnit.Framework;

namespace Unit.Domain;

public partial class UserShould
{
    [Test]
    public void have_a_name()
    {
        Scenario(() =>
        {
            Given(valid_inputs);
            And(a_null_user_name);
            When(Validating(creating_a_user));
            Then(Informs("Name cannot be empty"));
        });        
        
        Scenario(() =>
        {
            Given(valid_inputs);
            And(an_empty_user_name);
            When(Validating(creating_a_user));
            Then(Informs("Name cannot be empty"));
        });
        
        Scenario(() =>
        {
            Given(valid_inputs);
            And(a_user_name_with_non_alphabetical_characters);
            When(Validating(creating_a_user));
            Then(Informs("Name can only have alphabetical characters"));
        }); 
    }    
    
    [Test]
    public void have_an_email()
    {
        Scenario(() =>
        {
            Given(valid_inputs);
            And(a_null_email);
            When(Validating(creating_a_user));
            Then(Informs("Email cannot be empty"));
        });        
        
        Scenario(() =>
        {
            Given(valid_inputs);
            And(an_empty_email);
            When(Validating(creating_a_user));
            Then(Informs("Email cannot be empty"));
        });
        
        Scenario(() =>
        {
            Given(valid_inputs);
            And(an_invalid_email);
            When(Validating(creating_a_user));
            Then(Informs("Email must be valid"));
        });
    }

    [Test]
    public void not_have_the_forbidden_email()
    {
        Given(valid_inputs);
        And(a_forbidden_email);
        When(Validating(creating_a_user));
        Then(Informs("You are not allowed to register"));
    }
    
    [Test]
    public void not_have_the_forbidden_email_that_is_all_capitalised()
    {
        Given(valid_inputs);
        And(a_forbidden_email_that_is_all_capitalised);
        When(Validating(creating_a_user));
        Then(Informs("You are not allowed to register"));
    }

    [Test]
    public void create_valid_user()
    {
        Given(valid_inputs);
        When(creating_a_user);
        Then(the_user_is_created);
    }
}