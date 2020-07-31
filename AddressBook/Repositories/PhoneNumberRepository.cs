using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AddressBook.Interfaces;
using AddressBook.Models;

namespace AddressBook.Repositories
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly string _connectionString;

        public PhoneNumberRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AddressBook"].ConnectionString; ;
        }

        public IEnumerable<PhoneNumber> GetUserNumbers(int userId)
        {

            var numbers = new List<PhoneNumber>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[GetUserNumbers]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var number = new PhoneNumber
                        {
                            Id = int.Parse(reader["ID"].ToString()),
                            UserId = int.Parse(reader["UserID"].ToString()),
                            Number = reader["Number"].ToString(),
                            IsActive = (bool)reader["IsActive"]
                        };
                        numbers.Add(number);
                    }
                }
            }
            return numbers;
        }

        public PhoneNumber GetNumber(int id)
        {
            PhoneNumber number = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[GetNumberById]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        number = new PhoneNumber
                        {
                            Id = int.Parse(reader["ID"].ToString()),
                            UserId = int.Parse(reader["UserID"].ToString()),
                            Number = reader["Number"].ToString(),
                            IsActive = (bool)reader["IsActive"]
                        };
                    }
                }
            }
            return number;
        }

        public void AddNumber(PhoneNumber number)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[AddNumber]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = number.UserId;
                    command.Parameters.Add("@Number", SqlDbType.NVarChar).Value = number.Number;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = number.IsActive;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditNumber(PhoneNumber number)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[EditNumber]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = number.Id;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = number.UserId;
                    command.Parameters.Add("@Number", SqlDbType.NVarChar).Value = number.Number;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = number.IsActive;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteNumber(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[DeleteNumber]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAllNumbers(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[DeleteUserNumbers]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}