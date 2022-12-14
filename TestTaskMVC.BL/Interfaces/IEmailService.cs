using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMVC.DomainModels.Models;

namespace TestTaskMVC.BL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(AppUser appUser, string html, string content);
    }
}
