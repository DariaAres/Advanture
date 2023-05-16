using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.Http;
using Rovie.Data;

namespace Rovie.Areas.Identity.Pages.Account
{
    public class CreateRoom : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly ILogger<CreateRoom> _logger;
        private static SmtpClient smtp;
        public CreateRoom(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<CreateRoom> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
        }

        public string ReturnUrl { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name{get; set;}

        public async Task<IActionResult> OnPostOnClickAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Невозможно загрузить пользователя с идентификатором '{_userManager.GetUserId(User)}'.");
            }

            Console.WriteLine(await _userManager.GetEmailAsync(user));

            SendEmailAsync(await _userManager.GetEmailAsync(user)).GetAwaiter();
            return RedirectToPage();
        }
        private static async Task SendEmailAsync(string email)
        {
            try
            {
                MailAddress from = new MailAddress("rovie.sup@gmail.com", "ROVIE support");
                MailAddress to = new MailAddress(email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = $"Ключь для доступа к вашей комнате";
                m.Body = "Ключ: " + Key.GetUniqueKey(10);
                smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from.Address, "kmpiknnqigrvyhej");
                await smtp.SendMailAsync(m);
                Console.WriteLine("Письмо отправлено");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

}

