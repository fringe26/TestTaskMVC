using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTaskMVC.BL.Interfaces;
using TestTaskMVC.DomainModels.Models;
using TestTaskMVC.PL.ViewModels.AccountsVM;

namespace TestTaskMVC.PL.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet("account/sign_up")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("account/sign_up")]
        public async Task<IActionResult> Registration(RegistrationVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.UserName
            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var link = Url.Action(nameof(VerifyEmail), "Account", new { userId = appUser.Id, token = code }, Request.Scheme, Request.Host.ToString());
            string html = $" Click Here {link}";
            await _emailService.SendEmailAsync(registerVM.Email, null, html, null);
            return RedirectToAction(nameof(SendVerifyEmail));
        }

        public IActionResult SendVerifyEmail()
        {
            return View();
        }

        [HttpGet("/account/sign_in")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/account/sign_in")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View();
            }

            if (!await _userManager.CheckPasswordAsync(appUser, loginVM.Password))
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View();
            }
            if (!await _userManager.IsEmailConfirmedAsync(appUser))
            {
                ModelState.AddModelError("", "Please first email confirm");
                return View();
            }
           var signInResult =
                await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, false,true);

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Account is blocked");
                return View();
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View();
            }
            return RedirectToAction("AllClients", "Client");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgotPassword)
        {
            if (!ModelState.IsValid) return View(forgotPassword);
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user is null)
            {
                ModelState.AddModelError("", "User with this Email Doesn't Exist!");
                return View(forgotPassword);
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token = code }, Request.Scheme, Request.Host.ToString());
            string html = $"{link}";
            string content = "Reset Password";
            await _emailService.SendEmailAsync(user.Email, user.UserName, html, content);
            return RedirectToAction(nameof(RecoverPasswordView));
        }

        public IActionResult BeenReseted()
        {
            return View();
        }

        public IActionResult RecoverPasswordView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            var resetPasswordModel = new ResetPasswordVM { Email = email, Token = token };
            return View(resetPasswordModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid) return View(resetPasswordVM);
            var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Token, resetPasswordVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(resetPasswordVM);
            }
            return RedirectToAction(nameof(BeenReseted));

        }
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest();
            }
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                return BadRequest();
            }
            await _userManager.ConfirmEmailAsync(appUser, token);
            await _signInManager.SignInAsync(appUser, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
