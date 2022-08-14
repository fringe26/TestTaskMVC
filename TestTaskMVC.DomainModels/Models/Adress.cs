using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskMVC.DomainModels.Models
{
    public class Adress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalIndex { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

    }
}