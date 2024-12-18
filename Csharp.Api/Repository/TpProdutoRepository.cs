using Csharp.Api.Data;
using Csharp.Api.Model;
using Csharp.Api.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace Csharp.Api.Repository
{
    public class TpProdutoRepository : IDbMetodo
    {
        private readonly CommandSql _sql;
        private ReturnModel ret = new ReturnModel();

        public TpProdutoRepository(CommandSql sql)
        {
            _sql = sql;
        }

        public async Task<ReturnModel> Create(object model)
        {
            TipoProdutoModel tpProduto = (TipoProdutoModel)model;

            string query = $"use CRUDAngularC; Insert into TpProdutos(Nome)" +
                $" values(" +
                $"'{tpProduto.TipoProduto}')";
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
            return ret;

        }
        public async Task<ReturnModel> Delete(object model)
        {
            TipoProdutoModel produto = (TipoProdutoModel)model;
            string query = $"Use CRUDAngularC;Delete from TpProdutos where idTpProduto = {produto.IdTpProduto}";
            try
            {
                SqlConnection conn = new SqlConnection();
                using (conn = await _sql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);
                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0) { ret.Sucesso = true; }
                    else { ret.Sucesso = false; }
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

        public async Task<ReturnModel> SelectAll()
        {
            string query = "Use CRUDAngularC;Select * from TpProdutos";
            List<TipoProdutoModel> listProduto = new List<TipoProdutoModel>();
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
                        TipoProdutoModel model = new TipoProdutoModel
                        {
                            IdTpProduto = Convert.ToInt32(result["idTpProduto"]),
                            TipoProduto = result["Nome"].ToString(),
                        };
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
            var produtoBusca = (TipoProdutoModel)model;
            string query = $"Use CRUDAngularC;Select * from TpProdutos where idTpProduto = {produtoBusca.IdTpProduto}";
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
                        TipoProdutoModel TpProdutoResult = new TipoProdutoModel
                        {
                            IdTpProduto = Convert.ToInt32(result["idTpProduto"]),
                            TipoProduto = result["Nome"].ToString(),
                        };
                        ret.Objeto = TpProdutoResult;
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
            TipoProdutoModel produto = (TipoProdutoModel)model;
            string query = $"use CRUDAngularC; Upadate TpProdutos SET" +
                  $" Nome = '{produto.TipoProduto}'" +
                  $"where idTpProduto = {produto.IdTpProduto} ";

            try
            {
                SqlConnection conn = new SqlConnection();
                using (conn = await _sql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);
                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0) { ret.Sucesso = true; }
                    else { ret.Sucesso = false; }
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
    }
}
