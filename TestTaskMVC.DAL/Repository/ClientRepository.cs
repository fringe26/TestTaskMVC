using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMVC.DAL.DataBase;
using TestTaskMVC.DAL.IRepository;
using TestTaskMVC.DomainModels.Models;

namespace TestTaskMVC.DAL.Repository
{
    public class ClientRepository : EfCoreRepository<Client>,IClientRepository
    {
        

        public ClientRepository(ApplicationDbContext context) :base(context)
        {
            
        }

        public override async Task<Client> DetailsById(int? id)
        {
            var client = await _context.Clients.Include(a=>a.Adress).FirstOrDefaultAsync(i=>i.Id== id);
            return client;
        }

        public override async Task<bool> AddAsync(Client item)
        {
            try
            {
                item.AdressId = 2;
                await _context.Clients.AddAsync(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
