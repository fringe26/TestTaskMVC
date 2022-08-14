using System.ComponentModel.DataAnnotations;


namespace TestTaskMVC.PL.ViewModels.AccountsVM
{
    public class ForgetPasswordVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
