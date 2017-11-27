using SENAI.ExemploStartUp.Dominio.Servicos;
using SENAI.ExemploStartUp.Models.Models;
using SENAI.ExemploStartUp.UI.Site.ViewModels.Carrinho;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SENAI.ExemploStartUp.UI.Site.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly CarrinhoServico carrinhoServico;

        public CarrinhoController()
        {
            carrinhoServico = new CarrinhoServico();
        }

        // GET: Carrinho
        public ActionResult Index()
        {
            var produtos = carrinhoServico.RetornarProdutos();

            return View("Index",produtos);
        }

        public ActionResult DetalharProduto(Guid id)
        {
            ProdutoServico ps = new ProdutoServico();
            var produto = ps.RetornarPorId(id);
            ProdutoDetalheViewModel pv = new ProdutoDetalheViewModel();
            pv.Produto = new Produto();
            pv.Produto = produto;
            return View("DetalheProduto",pv);
        }

        public ActionResult Adicionar(ProdutoDetalheViewModel model)
        {

            Cliente cliente = (Cliente)Session["SessionCliente"];
            carrinhoServico.AdicionarAoCarrinho(model.Produto.ProdutoId, model.Quantidade, cliente);

          return  RedirectToAction("ListarItensCarrinhoAtivo");
        }

        public ActionResult ListarItensCarrinhoAtivo()
        {
            Cliente cliente = (Cliente)Session["SessionCliente"];

            CarrinhoViewModel carrinho = new CarrinhoViewModel();


            carrinho.Carrinho = new CarrinhoDeCompras();
             var carrinhoRetorno = carrinhoServico.RetornarCarrinho(cliente.ClienteId);
            if (carrinhoRetorno != null)
            {
                carrinho.Carrinho = carrinhoRetorno;
                carrinho.ItensCarrinho = new List<ItemCarrinho>();
                carrinho.ItensCarrinho = carrinhoServico.RetornarItenCarrinho(carrinho.Carrinho.CarrinhoDeComprasId);
                carrinho.ValorTotal = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", carrinho.Carrinho.ValorTotal);
                return View("ListarItensCarrinhoAtivo", carrinho);
            }

                TempData["CarrinhoVazio"] = "Você ainda não possuí itens adicionados ao carrinho.";
           return RedirectToAction("Index", "Cliente");
        }

        public ActionResult FinalizarCompra(Guid id)
        {
            CarrinhoDeCompras carrinho = new CarrinhoDeCompras();
            carrinho = carrinhoServico.FinalizarCompra(id);
            return RedirectToAction("DetalheCompra",carrinho);
        }

        public ActionResult DetalheCompra(CarrinhoDeCompras carrinho)
        {
            Cliente cliente = (Cliente)Session["SessionCliente"];

            var detalheCompraViewModel = new DetalheCompraViewModel();
            detalheCompraViewModel.DataCompra = carrinho.DataCompra;
            detalheCompraViewModel.Pedido = carrinho.CarrinhoDeComprasId.ToString();
            detalheCompraViewModel.NomeCliente = cliente.Nome;
            return View("DetalheCompra", detalheCompraViewModel);
        }

        public ActionResult RemoverDoCarrinho(Guid id)
        {
            carrinhoServico.RemoverDoCarrinho(id);
            return RedirectToAction("ListarItensCarrinhoAtivo");
        }


        [HttpPost]
        public ActionResult MeusPedidos(Guid id)
        {
           var resultado = carrinhoServico.RetornaPedidos(id).ToList();
            return View("MeusPedidos", resultado);
        }

        [HttpPost]
        public ActionResult TodosOsPedidos()
        {
            var Pedidos = carrinhoServico.TodosOsPedidos().ToList();
            return View(Pedidos.ToList());
        }
    }
}