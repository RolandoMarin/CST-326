using System.ComponentModel.DataAnnotations;

namespace CST_326.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string PhoneNumber { get; set; }
    }
}
