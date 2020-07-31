using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid phone number")] 
        [Display(Name = "Phone number")]
        public string Number { get; set; }

        [Display(Name = "Is active")]
        public bool IsActive { get; set; }
    }
}