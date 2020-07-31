using System.Collections.Generic;
using AddressBook.Models;

namespace AddressBook.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsersForAddress(int num);
        User GetUser(int id);
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(int id);
    }
}
