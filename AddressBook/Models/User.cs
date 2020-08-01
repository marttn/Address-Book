using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class User
    {
        public int Id { get; set; }

        [RegularExpression(@"^\w{1,50}$", ErrorMessage = "Please enter a valid first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^\w{1,50}$", ErrorMessage = "Please enter a valid last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public int Address { get; set; }
    }
}