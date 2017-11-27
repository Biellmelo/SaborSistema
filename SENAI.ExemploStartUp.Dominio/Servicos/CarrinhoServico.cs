using SENAI.ExemploStartUp.Data.Repositorios;
using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Dominio.Servicos
{
    public class CarrinhoServico
    {
        CarrinhoRepositorio cr = new CarrinhoRepositorio();

        public List<Produto> RetornarProdutos()
        {
            return cr.RetornarProdutos();
        }

        public List<Produto> RetornarProdutosPorCategoria(Guid idCategoria)
        {
            return cr.RetornarProdutosPorCategoria(idCategoria);
        }

        public List<ItemCarrinho> AdicionarAoCarrinho(Guid itemId,int Quantidade, Cliente cliente)
        {
            var carrinho = cr.VerificarCarrinhoExistente(cliente.ClienteId);
            if (carrinho != null)
            {
                ItemCarrinho item = new ItemCarrinho();
                item.ProdutoId = itemId;
                item.CarrinhoDeComprasId = carrinho.CarrinhoDeComprasId;
                item.Quantidade = Quantidade;
                return cr.AdicionarAoCarrinho(item);
            }
            else
            {
                CarrinhoDeCompras NovoCarrinho = new CarrinhoDeCompras();
                NovoCarrinho.ClienteId = cliente.ClienteId;
                NovoCarrinho.DataCompra = DateTime.Now;
                NovoCarrinho.StatusId = 0;
                NovoCarrinho = cr.CriarCarrinho(NovoCarrinho);

                ItemCarrinho item = new ItemCarrinho();
                item.ProdutoId = itemId;
                item.CarrinhoDeComprasId = NovoCarrinho.CarrinhoDeComprasId;
                item.Quantidade = Quantidade;
                return cr.AdicionarAoCarrinho(item);
            }

        }

        public List<ItemCarrinho> RemoverDoCarrinho(Guid itemId)
        {
            return cr.RemoverDoCarrinho(itemId);
        }

        public CarrinhoDeCompras FinalizarCompra(Guid carrinhoId)
        {
           var carrinho = cr.BuscarCarrinhoPorId(carrinhoId);
            carrinho.StatusId = 1;
            carrinho.DataCompra = DateTime.Now;
            return cr.FinalizarCompra(carrinho);
        }

        public CarrinhoDeCompras RetornarCarrinho(Guid clienteId)
        {
            return cr.VerificarCarrinhoExistente(clienteId);
        }

        public List<ItemCarrinho> RetornarItenCarrinho(Guid carrinhoId)
        {
            return cr.RetornarItensCarrinho(carrinhoId);
        }

        public List<CarrinhoDeCompras> MinhasVendas(Guid id)
        {
            CarrinhoRepositorio carrinhoRepositorio = new CarrinhoRepositorio();

            return carrinhoRepositorio.PedidosRealizado(id);
        }

        public List<CarrinhoDeCompras> RetornaPedidos(Guid cliente)
        {
            CarrinhoRepositorio carrinhoRepositorio = new CarrinhoRepositorio();
            return carrinhoRepositorio.RetornaPedidos(cliente);
        }
        public List<CarrinhoDeCompras> TodosOsPedidos()
        {
            CarrinhoRepositorio carrinhoRepositorio = new CarrinhoRepositorio();
             return carrinhoRepositorio.TodosOsPedidos();
        }
    }
}
