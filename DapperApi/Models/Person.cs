namespace DapperApi.Model;

public class Person
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime RegistrationDate { get; set; }
}
