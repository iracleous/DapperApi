using DapperApi.Model;

namespace DapperApi.Services;

public interface IPersonCrud
{
    Person CreatePerson(Person person);
    bool CreateTable();
    bool DeletePerson(long personId);
    void DeleteTable();
    bool ExecuteCommand(string queryString);
    Person? GetPerson(long personId);
    IEnumerable<Person>? GetPerson();
    IEnumerable<Person>? GetPersonDetails();
    bool UpdatePerson(long personId, Person person);
}