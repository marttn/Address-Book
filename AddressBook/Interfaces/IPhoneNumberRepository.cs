using System.Collections.Generic;
using AddressBook.Models;

namespace AddressBook.Interfaces
{
    public interface IPhoneNumberRepository
    {
        IEnumerable<PhoneNumber> GetUserNumbers(int userId);
        PhoneNumber GetNumber(int id);
        void AddNumber(PhoneNumber number);
        void EditNumber(PhoneNumber number);
        void DeleteNumber(int id);
        void DeleteAllNumbers(int userId);
    }
}
