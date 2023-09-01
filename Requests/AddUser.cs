using System.ComponentModel.DataAnnotations;

namespace Event_Management.Requests{
    public class AddUser{
        [Required]
        public string Name {get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email {get; set; }= string.Empty;
        [Required]
        public int PhoneNumber {get; set; }
    }
}