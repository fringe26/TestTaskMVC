using System.ComponentModel.DataAnnotations;

namespace TestTaskMVC.PL.ViewModels
{
    public class ClientVM
    {
        [Required] 
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Company { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
