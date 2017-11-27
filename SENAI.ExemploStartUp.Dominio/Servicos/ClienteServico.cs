using SENAI.ExemploStartUp.Data.Repositorios;
using SENAI.ExemploStartUp.Models.Models;
using SENAI.ExemploStartUp.Util.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENAI.ExemploStartUp.Dominio.Servicos
{
    public class ClienteServico
    {
        public ClienteServico()
        {

        }

        public Cliente EfetuarLogin(string email, string senha)
        {
            ClienteRepositorio clienteRepositorio = new ClienteRepositorio();
            Cliente cliente = clienteRepositorio.EfetuarLogin(email, Criptografia.GetMD5Hash(senha));

            return cliente;

        }

        public void InserirCliente(Cliente cliente)
        {
            ClienteRepositorio cr = new ClienteRepositorio();
            cliente.Senha = Criptografia.GetMD5Hash(cliente.Senha);
            cr.InserirCliente(cliente);
        }

        //public void VerificarEmail(string Email)
        //{
        //    ClienteRepositorio clienteRepositorio = new ClienteRepositorio();
        //    var Email = clienteRepositorio.GettAll();


            
        //}

        public bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

    
        
    }
}
