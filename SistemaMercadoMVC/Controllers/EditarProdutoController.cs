using Microsoft.AspNetCore.Mvc;
using SistemaMercadoMVC.Data;
using SistemaMercadoMVC.Models;
using System.Reflection.Metadata.Ecma335;

namespace SistemaMercadoMVC.Controllers
{
    [Route("EditarProduto/{id:int}")]
    public class EditarProdutoController : Controller
    {
        private readonly AppDbContext ctx;
        public EditarProdutoController(AppDbContext context)
        {
            ctx = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null || usuarioId <= 0)
               return RedirectToAction("Index", "Login");


            if (id == 0)
                return NotFound();

            Produto produtoCarregado = ctx.Produto.Find(id);
            if (produtoCarregado == null)
                return NotFound();

            return View(produtoCarregado);
        }

        [HttpPost]
        [Route("Salvar")]
        public IActionResult CadastrarProduto(int id, string nome, string descricao, decimal preco, IFormFile imagem)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao) || preco <= 0 || imagem == null)
            {
                ViewBag.Error = "Todos os campos são obrigatórios!";
                return View("Index");
            }
            try
            {
                byte[] bytesImagem;
                using (var memoryStream = new MemoryStream())
                {
                    imagem.CopyTo(memoryStream);
                    bytesImagem = memoryStream.ToArray();
                }

                Produto produto = ctx.Produto.Find(id);

                if (produto == null)
                    return NotFound();

                produto.NomeProduto = nome;
                produto.Descricao = descricao;
                produto.Preco = preco;
                produto.Imagem = bytesImagem;

                ctx.Produto.Update(produto);
                ctx.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro ao editar o produto!";
                return View("Index");
            }
        }
    }
}
