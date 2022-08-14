using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestTaskMVC.DomainModels.Models
{
    public class AppUser :IdentityUser
    {
        [Required]
        [StringLength(maximumLength: 255)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 255)]
        
        public string Surname { get; set; }
        [Required]
        public bool IsActivated { get; set; }
        [NotMapped]
        public IEnumerable<Client> Clients { get; set; }
    }
}
