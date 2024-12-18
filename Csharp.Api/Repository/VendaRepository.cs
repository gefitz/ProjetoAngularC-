using Csharp.Api.Data;
using Csharp.Api.Model;
using Csharp.Api.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace Csharp.Api.Repository
{
    public class VendaRepository : IDbMetodo
    {
        private readonly CommandSql _sql;
        private ReturnModel ret = new ReturnModel();

        public VendaRepository(CommandSql sql)
        {
            _sql = sql;
        }
        public async Task<ReturnModel> Create(object model)
        {
            VendaModel venda = (VendaModel)model;
            for (int i = 0; i < venda.Produto.Count(); i++)
            {
                var produto = venda.Produto[i];
                float vlrTotal = produto.vlrProduto * venda.qtdVendida;
                string query = $"use CRUDAngularC; Insert into Produtos" +
                    $" (idProduto,qtdVendida,dthVenda) values(" +
                    $"'{produto.IdProduto}'," +
                    $"'{venda.qtdVendida}'," +
                    $"{vlrTotal.ToString("F2", new CultureInfo("en-US"))}," +
                    $"'{venda.dthVenda}')";
                try
                {

                    SqlConnection con = new SqlConnection();
                    using (con = await _sql.Open(con))
                    {
                        var command = new SqlCommand(query, con);
                        var result = await command.ExecuteNonQueryAsync();
                        if (result != 0)
                        {
                            ret.Sucesso = true;
                        }
                        else
                        {
                            ret.Sucesso = false;
                            ret.Mensagem = "Erro ao tentar inserir";
                        }

                    }

                }
                catch (Exception ex)
                {
                    ret.Sucesso = false;
                    ret.Mensagem = ex.Message;
                }
            }

            return ret;
        }

        public async Task<ReturnModel> Delete(object model)
        {
            return ret;
        }

        public async Task<ReturnModel> SelectAll()
        {
            string query = "Use CRUDAngularC;Select * from Vendas";
            List<VendaModel> listProduto = new List<VendaModel>();
            List<ProdutoModel> produtos = new List<ProdutoModel>();
            try
            {
                SqlConnection conn = new SqlConnection();
                using (conn = await _sql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);

                    var result = await command.ExecuteReaderAsync();
                    if (result == null) { ret.Sucesso = false; ret.Mensagem = "Não foi encontrado Nenhum produto"; }
                    while (result.Read())
                    {
                        VendaModel model = new VendaModel
                        {
                            IdVenda = Convert.ToInt32(result["idVenda"]),
                            qtdVendida = Convert.ToInt32(result["qtdVendida"].ToString()),
                            dthVenda = DateTime.Parse(result["dthVenda"].ToString()),
                        };
                        produtos.Add(new ProdutoModel { IdProduto = Convert.ToInt32(result["idProduto"]) });
                        model.Produto = produtos;  
                        listProduto.Add(model);
                    }
                    ret.Objeto = listProduto;
                    ret.Sucesso = true;
                    _sql.Close(conn);
                }

            }
            catch (Exception ex)
            {
                ret.Sucesso = false;
                ret.Mensagem = ex.Message;

            }
            return ret;
        }

        public async Task<ReturnModel> SelectBy(object model)
        {
            var produtoBusca = (ProdutoModel)model;
            string query = $"Use CRUDAngularC;Select * from Vendas where idVenda = {produtoBusca.IdProduto}";
            List<ProdutoModel> produtos = new List<ProdutoModel> ();
            try
            {
                SqlConnection conn = new SqlConnection();
                using (conn = await _sql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);

                    var result = await command.ExecuteReaderAsync();
                    if (result == null) { ret.Sucesso = false; ret.Mensagem = "Não foi encontrado Nenhum produto"; }
                    while (result.Read())
                    {
                        VendaModel venda = new VendaModel
                        {
                            IdVenda = Convert.ToInt32(result["idVenda"]),
                            qtdVendida = Convert.ToInt32(result["qtdVendida"].ToString()),
                            dthVenda = DateTime.Parse(result["dthVenda"].ToString()),
                        };
                        produtos.Add(new ProdutoModel { IdProduto = Convert.ToInt32(result["idProduto"]) });
                        venda.Produto = produtos;
                        ret.Objeto = venda;
                    }
                    ret.Sucesso = true;
                    _sql.Close(conn);
                }

            }
            catch (Exception ex)
            {
                ret.Sucesso = false;
                ret.Mensagem = ex.Message;

            }
            return ret;

        }

        public async Task<ReturnModel> Update(object model)
        {
            return ret;
        }

    }
}
