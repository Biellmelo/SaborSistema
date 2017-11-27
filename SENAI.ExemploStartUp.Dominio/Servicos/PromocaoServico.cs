using SENAI.ExemploStartUp.Data.Repositorios;
using SENAI.ExemploStartUp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Dominio.Servicos
{
    public class PromocaoServico
    {
        PromocaoRepositorio promocaoRepositorio = new PromocaoRepositorio();


        public void NovaPromocao(Promocao promocao)
        {
            promocaoRepositorio.NovaPromocao(promocao);
        }

        public List<Promocao> PromocaoDoDia(string Dia)
        {
            return promocaoRepositorio.PromocaoDoDia(Dia);
        }

    }
}
