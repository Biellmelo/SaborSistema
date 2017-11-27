using SENAI.ExemploStartUp.Data.Context;
using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Data.Repositorios
{
    public class CarrinhoRepositorio
    {
        private ExemploStartUpContext db = new ExemploStartUpContext();

        public List<Produto> RetornarProdutos()
        {
            return db.Produtos.Where(p => p.Ativo == true).ToList();
        }

        public List<Produto> RetornarProdutosPorCategoria(Guid idCategoria)
        {
            return db.Produtos.Where(p => p.CategoriaId == idCategoria).ToList();
        }

        public List<ItemCarrinho> AdicionarAoCarrinho(ItemCarrinho item)
        {
            db.ItensCarrinho.Add(item);
            db.SaveChanges();
            AtualizarValorTotalCarrinho(item.CarrinhoDeComprasId);
            return db.ItensCarrinho.Include("Produto").Where(i => i.CarrinhoDeComprasId == item.CarrinhoDeComprasId).ToList();
        }

        public CarrinhoDeCompras VerificarCarrinhoExistente(Guid clienteId)
        {
            return db.Carrinhos.Where(c => c.StatusId == 0 && c.ClienteId == clienteId).FirstOrDefault();
        }

        public List<ItemCarrinho> RemoverDoCarrinho(Guid itemId)
        {
            var item = db.ItensCarrinho.Where(i => i.ItemCarrinhoId == itemId).FirstOrDefault();
            db.ItensCarrinho.Remove(item);
            db.SaveChanges();
            AtualizarValorTotalCarrinho(item.CarrinhoDeComprasId);
            return db.ItensCarrinho.Where(i => i.CarrinhoDeComprasId == item.CarrinhoDeComprasId).ToList();
        }

        public CarrinhoDeCompras CriarCarrinho(CarrinhoDeCompras carrinho)
        {
            db.Carrinhos.Add(carrinho);
            db.SaveChanges();
            return VerificarCarrinhoExistente(carrinho.ClienteId);

        }

        public CarrinhoDeCompras FinalizarCompra(CarrinhoDeCompras carrinho)
        {
            db.Entry(carrinho).State = EntityState.Modified;
            db.SaveChanges();
            return carrinho;
        }

        public List<ItemCarrinho> RetornarItensCarrinho(Guid carrinhoId)
        {
            return db.ItensCarrinho.Where(c => c.CarrinhoDeComprasId == carrinhoId).ToList();
        }

        public void AtualizarValorTotalCarrinho(Guid carrinhoId)
        {
            var itens = db.ItensCarrinho.Include("Produto").Where(c => c.CarrinhoDeComprasId == carrinhoId).ToList();

            double somaItens = 0;
            foreach (var item in itens)
            {
                somaItens = somaItens + (item.Produto.Preco * item.Quantidade);
            }

            CarrinhoDeCompras carrinho = new CarrinhoDeCompras();
            carrinho = db.Carrinhos.Where(c => c.CarrinhoDeComprasId == carrinhoId).FirstOrDefault();
            carrinho.ValorTotal = somaItens;
            db.Entry(carrinho).State = EntityState.Modified;
            db.SaveChanges();
        }

        public CarrinhoDeCompras BuscarCarrinhoPorId(Guid carrinhoId)
        {

            return db.Carrinhos.Where(c => c.CarrinhoDeComprasId == carrinhoId).FirstOrDefault();
        }

        public List<CarrinhoDeCompras> PedidosRealizado(Guid cliente)
        {

            return db.Carrinhos.Where(c => c.ClienteId == cliente).ToList();
        }

        public List<CarrinhoDeCompras> RetornaPedidos(Guid cliente)
        {
            return db.Carrinhos.Where(c => c.ClienteId == cliente).ToList();
        }

        public List<CarrinhoDeCompras> TodosOsPedidos()
        {
            return db.Carrinhos.ToList();
        }
    }
}