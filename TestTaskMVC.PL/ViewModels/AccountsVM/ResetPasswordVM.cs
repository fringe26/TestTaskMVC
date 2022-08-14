﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskMVC.PL.ViewModels.AccountsVM
{
    public class ResetPasswordVM
    {
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
