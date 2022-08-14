using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskMVC.BL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string mail, string username, string html, string content);
    }
}
