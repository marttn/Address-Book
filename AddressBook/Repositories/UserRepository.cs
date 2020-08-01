using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AddressBook.Interfaces;
using AddressBook.Models;

namespace AddressBook.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AddressBook"].ConnectionString;

        }

        public IEnumerable<User> GetUsersForAddress(int num)
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[GetUsers]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AddressNum", num);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = int.Parse(reader["ID"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Address = int.Parse(reader["Address"].ToString())
                        };
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public User GetUser(int id)
        {
            User user = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[GetUserById]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            Id = int.Parse(reader["ID"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString()
                        };
                    }
                }
            }
            return user;
        }

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[AddUser]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
                    command.Parameters.Add("@Address", SqlDbType.Int).Value = user.Address;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[EditUser]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("[DeleteUser]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}