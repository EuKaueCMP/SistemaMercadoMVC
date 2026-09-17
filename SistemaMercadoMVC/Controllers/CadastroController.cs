using Microsoft.AspNetCore.Mvc;
using SistemaMercadoMVC.Data;
using SistemaMercadoMVC.Models;
using System.Runtime.CompilerServices;

namespace SistemaMercadoMVC.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AppDbContext ctx;
        public CadastroController(AppDbContext context)
        {
            ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(string nome, string email, string senha)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ViewBag.Error = "Todos os campos são obrigatórios!";
                return View("Index");
            };

            byte[] senhaHash = HashService.Hash(senha);
            Usuario usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senhaHash,
            };

            ctx.Usuario.Add(usuario);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
