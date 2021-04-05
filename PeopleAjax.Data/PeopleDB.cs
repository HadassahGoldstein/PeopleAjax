using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PeopleAjax.Data
{
    public class PeopleDB
    {
        private readonly string _connectionString;
        public PeopleDB(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPerson(Person p)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO People (firstName,lastName,age)
                                    VALUES (@firstName,@lastName,@age)";
            command.Parameters.AddWithValue("@firstName", p.FirstName);
            command.Parameters.AddWithValue("@lastName", p.LastName);
            command.Parameters.AddWithValue("@age", p.Age);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public List<Person> GetPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM People";
            connection.Open();
            List<Person> ppl = new List<Person>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    FirstName = (string)reader["firstName"],
                    LastName = (string)reader["lastName"],
                    Age = (int)reader["age"],
                    Id = (int)reader["id"]
                });
            }
            return ppl;
        }
        public void DeletePerson(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM People WHERE id=@id";
            command.Parameters.AddWithValue("@id", id);            
            connection.Open();
            command.ExecuteNonQuery();
        }
        public void EditPerson(Person p)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE People SET firstName=@firstName,lastName=@lastName, age=@age WHERE id=@id ";
            command.Parameters.AddWithValue("@id", p.Id);
            command.Parameters.AddWithValue("@firstName", p.FirstName);
            command.Parameters.AddWithValue("@lastName", p.LastName);
            command.Parameters.AddWithValue("@age", p.Age);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
