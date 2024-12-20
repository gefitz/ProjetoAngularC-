using Csharp.Api.Data;
using Csharp.Api.Model;
using Csharp.Api.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace Csharp.Api.Repository
{
    public class ProdutoRepository : IDbMetodo
    {
        private readonly CommandSql _sql;
        private ReturnModel ret = new ReturnModel();

        public ProdutoRepository(CommandSql sql)
        {
            _sql = sql;
        }

        public async Task<ReturnModel> Create(object model)
        {
            ProdutoModel produto = (ProdutoModel)model;
            produto.dthAlteracaoProduto = DateTime.Now;
            produto.dthCriadoProduto = DateTime.Now;
            string query = $"use CRUDAngularC; Insert into Produtos" +
                $" (Nome,Descricao,qtdProduto,vlrProduto,TpProduto,dthCriacaoProduto,dthAlteraoProduto) values(" +
                $"'{produto.Nome}'," +
                $"'{produto.Descricao}'," +
                $"{produto.qtdProduto}," +
                $"{produto.vlrProduto.ToString("F2", new CultureInfo("en-US"))}," +
                $"'{produto.tpProduto}'," +
                $"'{produto.dthCriadoProduto}'," +
                $"'{produto.dthAlteracaoProduto}')";
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
            ProdutoModel produto = (ProdutoModel)model;
            string query = $"Use CRUDAngularC;Delete from Produtos where idProduto = {produto.IdProduto}";
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
            string query = "Use CRUDAngularC;Select * from Produtos";
            List<ProdutoModel> listProduto = new List<ProdutoModel>();
            try
            {
                SqlConnection conn = new SqlConnection();
                using (conn = await _sql.Open(conn))
                {
                    var command = new SqlCommand(query, conn);

                    var result = await command.ExecuteReaderAsync();
                    if(result == null) {  ret.Sucesso = false; ret.Mensagem = "Não foi encontrado Nenhum produto"; }
                    while (result.Read())
                    {
                        ProdutoModel model = new ProdutoModel
                        {
                            IdProduto = Convert.ToInt32(result["idProduto"]),
                            Nome = result["Nome"].ToString(),
                            Descricao = result["Descricao"].ToString(),
                            vlrProduto = float.Parse(result["vlrProduto"].ToString()),
                            qtdProduto = Convert.ToInt32(result["qtdProduto"]),
                            dthCriadoProduto = DateTime.Parse(result["dthCriacaoProduto"].ToString()),
                            dthAlteracaoProduto = DateTime.Parse(result["dthAlteraoProduto"].ToString()),
                            tpProduto = result["TpProduto"].ToString()
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
            var produtoBusca = (ProdutoModel)model;
            string query = $"declare\n" +
                $"@nome varchar(255) = '{produtoBusca.Nome}' ," +
                $"@tpProduto varchar(255) = '{produtoBusca.tpProduto}';" +
                $" Use CRUDAngularC; " +
                $"Select* from Produtos " +
                $"where" +
                $"(@nome = '' OR Nome like '%' + @nome + '%')" +
                $"AND(@tpProduto = '' OR tpProduto = @tpProduto)";
            List<ProdutoModel> produtoList =new List<ProdutoModel> ();
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
                        ProdutoModel produtoResult = new ProdutoModel
                        {
                            IdProduto = Convert.ToInt32(result["idProduto"]),
                            Nome = result["Nome"].ToString(),
                            Descricao = result["Descricao"].ToString(),
                            vlrProduto = float.Parse(result["vlrProduto"].ToString()),
                            qtdProduto = Convert.ToInt32(result["qtdProduto"]),
                            dthCriadoProduto = DateTime.Parse(result["dthCriacaoProduto"].ToString()),
                            dthAlteracaoProduto = DateTime.Parse(result["dthAlteraoProduto"].ToString()),
                            tpProduto = result["TpProduto"].ToString()
                        };
                        produtoList.Add( produtoResult);
                    }
                    ret.Objeto = produtoList;
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
            ProdutoModel produto = (ProdutoModel)model;
            produto.dthAlteracaoProduto = DateTime.Now;
            string query = $"use CRUDAngularC; Update Produtos SET" +
                  $" Nome = '{produto.Nome}'" +
                  $",Descricao = '{produto.Descricao}'" +
                  $",qtdProduto = {produto.qtdProduto}" +
                  $",vlrProduto = {produto.vlrProduto.ToString("F2", new CultureInfo("en-US"))}" +
                  $",TpProduto = '{produto.tpProduto}'" +
                  $",dthAlteraoProduto = '{produto.dthAlteracaoProduto}' where idProduto = {produto.IdProduto} "; 

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
