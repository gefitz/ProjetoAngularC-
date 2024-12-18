﻿using Microsoft.Data.SqlClient;
using System.Security.Principal;

namespace Csharp.Api.Data.CriacaoBase
{
    public class CriarBase
    {
        private readonly CommandSql _commandSql;

        public CriarBase(CommandSql commandSql)
        {
            _commandSql = commandSql;
            GerarBase();
        }

        public async Task GerarBase()
        {
            string query = "Create DataBase CRUDAngularC";
            try
            {

                SqlConnection conn = new SqlConnection();
                using (conn = await _commandSql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);
                    await command.ExecuteNonQueryAsync();
                    await _commandSql.Close(conn);

                }
                if (await GerarTpProduto())
                {
                    if (await GerarProduto())
                    {
                        await GerarVenda();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("já existe")) 
                    return;
                return;
            }
        }

        private async Task<bool> GerarProduto()
        {
            bool ret = false;
            string query = "use CRUDAngularC; create table Produtos(" +
                "idProduto int primary key identity(1,1)," +
                "Nome varchar(255) not null," +
                "Descricao varchar(255)," +
                "qtdProduto int not null," +
                "vlrProduto float not null," +
                "dthCriacaoProduto datetime not null," +
                "dthAlteraoProduto datetime not null," +
                "idTpProduto int," +
                "constraint fk_idTpProduto_TpProduto foreign key (idTpProduto) references TpProdutos(idTpProduto))";

            try
            {

                SqlConnection conn = new SqlConnection();
                using (conn = await _commandSql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);
                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0) { ret = true; }
                    conn.Close();
                }
                return ret;
            }
            catch (Exception ex)
            {
                return ret;
            }

        }
        private async Task<bool> GerarVenda()
        {
            bool ret = false;
            string query = "use CRUDAngularC; create table Vendas(" +
                "idVenda int primary key identity(1,1)," +
                "idProduto int," +
                "qtdVendida int," +
                "dthVenda datetime," +
                "constraint fk_idProduto_Produtos foreign key (idProduto) references Produtos(idProduto))";

            try
            {


                SqlConnection conn = new SqlConnection();
                using (conn = await _commandSql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);
                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0) { ret = true; }
                    conn.Close();
                }
            }
            catch (Exception ex) { }
            return ret;
        }
        private async Task<bool> GerarTpProduto()
        {
            try
            {

                bool ret = false;
                string query = "USE CRUDAngularC;CREATE TABLE TpProdutos(idTpProduto INT PRIMARY KEY IDENTITY(1,1),Nome VARCHAR(255) NOT NULL);";

                SqlConnection conn = new SqlConnection();
                using (conn = await _commandSql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);
                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0) { ret = true; }
                    conn.Close();
                }
                return ret;
            }
            catch (Exception ex) { }
            return false;
        }
    }
}