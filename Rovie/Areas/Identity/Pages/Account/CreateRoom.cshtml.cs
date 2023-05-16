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

namespace Rovie.Areas.Identity.Pages.Account
{
    public class CreateRoom : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly ILogger<CreateRoom> _logger;
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
        public string Name {get;set;}
        private static string name;

        public async Task<IActionResult> OnPostOnClickAsync()
        {
            SendEmailAsync().GetAwaiter();
            return RedirectToPage();
        }
        private static async Task SendEmailAsync()
        {
            Console.WriteLine("krjfn");
            try
            {
                name = name ?? string.Empty;
                MailAddress from = new MailAddress("rovie.sup@gmail.com", "ROVIE support");
                MailAddress to = new MailAddress("kapoor.darya@gmail.com");
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Код доступа к вашей комнате " + name;
                m.Body = "Письмо-тест 2 работы smtp-клиента";
                SmtpClient smtp = new SmtpClient("smpt.gmail.com", 587);
                smtp.EnableSsl = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(m);

                smtp.Credentials = new NetworkCredential("ROVIE support", "theonegeltop");
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

