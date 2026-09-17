using Microsoft.AspNetCore.Mvc;
using SistemaMercadoMVC.Data;
using SistemaMercadoMVC.Models;

namespace SistemaMercadoMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext ctx;
        public LoginController(AppDbContext context)
        {
            ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ViewBag.Error = "Email ou senha inválidos!";
                return View("Index");
            }
            ;

            Usuario usuario = ctx.Usuario.FirstOrDefault(u => u.Email == email);
            if (usuario == null)
            {
                ViewBag.Error = "Email ou senha inválidos";
                return View("Index");
            }
            ;

            var senhaDigitadaHash = HashService.Hash(senha);
            if (!usuario.Senha.SequenceEqual(senhaDigitadaHash))
            {
                Console.WriteLine(senhaDigitadaHash);
                Console.WriteLine("erro foi aqui no hash");
                ViewBag.Error = "Email ou senha inválidos";

                return View("Index");
            };

            Console.WriteLine("Chegou aqui");
            return RedirectToAction("Index", "Home");
        }
    }
}
