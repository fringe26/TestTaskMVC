﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskMVC.BL.Interfaces;
using TestTaskMVC.DAL.DataBase;
using TestTaskMVC.DomainModels.Models;
using TestTaskMVC.PL.ViewModels;

namespace TestTaskMVC.PL.Controllers
{
    public class ClientController : Controller
    {

        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public ClientVM Client { get; set; }

        [HttpGet]
        public IActionResult AllClients()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClient()
        {
            if (!ModelState.IsValid) return View();
            Client client = new Client
            {
                Name = Client.Name,
                Surname = Client.Surname,
                Email = Client.Email,
                Company = Client.Company,
                Phone = Client.Phone,
                Adress = new Adress
                {
                    Country = Client.Country,
                    City = Client.City,
                    PostalIndex=Client.PostalIndex,
                    Street=Client.Street

                } 
               
            };
           
           
            
            
            await _clientService.AddClient(client);
             
                
             return RedirectToAction("AllClients");
            
            

        }



        //[BindProperty]
        //public ClientVM ClientVm { get; set; }
        //public ClientController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public IActionResult AllClients()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> CreateClient(int? id)
        {
            Client = new ClientVM();
           
            return View(Client);
          
        }



        //#region Api Calls
        [HttpGet("/Client/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _clientService.GetAllClients() });
        }

       
        public async Task<IActionResult> Details(int id)
        {
            var client = await _clientService.GetClient(id);
            return View(client);
        }

        //[HttpDelete("/client/delete{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var bookFromDb = await _context.Clients.FindAsync(id);
        //    if (bookFromDb == null)
        //    {
        //        return Json(new { success = false, message = "Error while Deleting" });
        //    }
        //    _context.Clients.Remove(bookFromDb);
        //    await _context.SaveChangesAsync();
        //    return Json(new { success = true, message = "Deleted Successful" });
        //}
        //#endregion
    }
}

