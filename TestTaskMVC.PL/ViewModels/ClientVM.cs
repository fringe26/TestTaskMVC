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
        [Required]

        public string Country { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Street { get; set; }
        [Required]

        public string PostalIndex { get; set; }
        
    }
}
