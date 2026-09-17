using Microsoft.AspNetCore.Mvc;
using SistemaMercadoMVC.Data;
using SistemaMercadoMVC.Models;

namespace SistemaMercadoMVC.Controllers
{
    public class CadastrarProdutoController : Controller
    {
        private readonly AppDbContext ctx;
        public CadastrarProdutoController(AppDbContext context)
        {
            ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProduto(string nome, string descricao, decimal preco, IFormFile imagem)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao) || preco <= 0 || imagem == null)
            {
                Console.WriteLine("Foiiiii aquiiii!");
                ViewBag.Erro = "Todos os campos são obrigatórios!";
                return View("Index", "CadastrarProduto");
            }

            try
            {
                byte[] bytesImagem;
                using (var memoryStream = new MemoryStream())
                {
                    imagem.CopyTo(memoryStream);
                    bytesImagem = memoryStream.ToArray();
                }
                Produto produto = new Produto
                {
                    NomeProduto = nome,
                    Descricao = descricao,
                    Preco = preco,
                    Imagem = bytesImagem
                };

                Console.WriteLine("Chegou aqui!");
                ctx.Produto.Add(produto);
                ctx.SaveChanges();

                return RedirectToAction("Index", "CadastrarProduto");
            }
            catch (Exception ex)
            {
                Console.WriteLine("O erro foi: " + ex);
                ViewBag.Erro = "Ocorreu um erro ao cadastrar o produto.";
                return View("Index");
            }
        }
    }
}
