using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMVC.BL.Interfaces;
using TestTaskMVC.DAL.IRepository;
using TestTaskMVC.DomainModels.Models;

namespace TestTaskMVC.BL.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddClient(Client client)
        {
            try
            {
                await _repository.AddAsync(client);
                return true;
            }
            catch
            {
                return false;
            }
            

        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            var clients = await _repository.GetAllAsync();
            return clients;
            
        }

        public async Task<Client> GetClient(int? id)
        {
            var client = await _repository.DetailsById(id);
            return client;
        }

        public async Task<bool> UpdateClient(Client client)
        {
            var IsUpdated = await _repository.Update(client);
            return IsUpdated;
        }
    }
}
