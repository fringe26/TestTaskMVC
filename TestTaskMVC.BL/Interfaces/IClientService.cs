using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMVC.DomainModels.Models;

namespace TestTaskMVC.BL.Interfaces
{
    public interface IClientService
    {
        public Task<IEnumerable<Client>> GetAllClients();
        public Task<Client> GetClient(int? id);
        public Task<bool> AddClient(Client client);
        public Task<bool> UpdateClient(Client client);
        public Task<bool> DeleteClient(int id);
    }
}
