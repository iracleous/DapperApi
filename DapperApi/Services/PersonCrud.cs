using Dapper;
using DapperApi.Model;
using System.Data.SqlClient;

namespace DapperApi.Services;

public class PersonCrud : IPersonCrud
{
    private static readonly string connectionString =
            "Data Source=(local); Initial Catalog=nbgEshopDb; Integrated Security = True; TrustServerCertificate=True; ";


    public bool ExecuteCommand(string queryString)
    {
        try
        {
            using var connection = new SqlConnection(connectionString);

            var command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool CreateTable()
    {
        string command = "create table person (Id bigint primary key identity,  Name nvarchar(50) ,Email  nvarchar(50),RegistrationDate date )";
        return ExecuteCommand(command);
    }

    public void DeleteTable()
    {
        string command = "drop table person;";
        ExecuteCommand(command);
    }

    public Person CreatePerson(Person person)
    {
        string command = $"insert into Person(Name, email, RegistrationDate) values(@Name,@Email,@RegDate); SELECT SCOPE_IDENTITY();";
        using SqlConnection connection = new(connectionString);
        connection.Open();
        long personId = connection.ExecuteScalar<long>(command, new { person.Name, person.Email, RegDate = person.RegistrationDate.ToString() });
        person.Id = personId;
        
        return person;
    }

    public Person? GetPerson(long personId)
    {
        using SqlConnection connection = new(connectionString);
        connection.Open();
        string query = "SELECT Id, Name, Email, RegistrationDate FROM Person WHERE Id = @Id";
        var person = connection.QueryFirstOrDefault<Person>(query, new { Id = personId.ToString() });
        return person;
    }

    public IEnumerable<Person>? GetPerson()
    {
        using SqlConnection connection = new(connectionString);
        connection.Open();
        string query = "SELECT Id, Name, Email, RegistrationDate FROM Person";
        var persons = connection.Query<Person>(query);
        return persons;
    }

    public IEnumerable<Person>? GetPersonDetails()
    {
        using SqlConnection connection = new(connectionString);
        connection.Open();
        string query = "SELECT Id, Name, Email FROM Person";
        var dynPersons = connection.Query(query);

        List<Person>? persons = [];
        foreach (var item in dynPersons)
        {
            persons.Add( new Person 
            { 
                Id = item.Id+100,
                Name = "Name = "+item.Name,
                Email = item.Email,
                RegistrationDate = item.RegistrationDate,
            
            } );
        }
        return persons;
    }

    public bool DeletePerson(long personId)
    {
        using SqlConnection connection = new(connectionString);
        connection.Open();
        string command = "DELETE FROM Person WHERE Id = @Id";
        var affectedRows = connection.Execute(command, new {Id = personId });
        return affectedRows > 0;
    }

    public bool UpdatePerson(long personId, Person person)
    {
        using SqlConnection connection = new(connectionString);
        connection.Open();
        string command = "UPDATE Person SET Name=@Name, Email=@Email, RegistrationDate=@RegDate  WHERE Id = @Id";
        var affectedRows = connection.Execute(command, new { 
            person.Name,
            person.Email,
            RegDate = person.RegistrationDate
            });
        return affectedRows > 0;
    }
}
