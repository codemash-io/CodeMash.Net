using CodeMash.Models;

namespace CodeMash.Tests;

[Collection("employees")]
public class Employee : Entity
{
    [Field("first_name")]
    public string FirstName { get; set; }
    [Field("last_name")]
    public string LastName { get; set; }


}