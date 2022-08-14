using System.ComponentModel.DataAnnotations;

namespace TestTaskMVC.PL.ViewModels.AccountsVM
{
    public class LoginVM
    {

        [Required]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
