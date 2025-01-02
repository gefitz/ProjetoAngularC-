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
        private readonly ProdutoRepository _produtoRepository;

        public VendaRepository(CommandSql sql, ProdutoRepository produtoRepository)
        {
            _sql = sql;
            _produtoRepository = produtoRepository;
        }
        public async Task<ReturnModel> Create(object model)
        {
            List<VendaModel> vendas = (List<VendaModel>)model;
            for (int i = 0; i < vendas.Count(); i++)
            {
                var produto = vendas[i].Produto;
                var venda = vendas[i];
                venda.dthVenda = DateTime.Now;
                string query = $"use CRUDAngularC; Insert into Vendas" +
                    $" (idProduto,qtdProdutoVendido,vlrTotal,dthVenda) values(" +
                    $"{produto.IdProduto}," +
                    $"{venda.qtdProdutoVendido}," +
                    $"{venda.vlrTotal.ToString("F2", new CultureInfo("en-US"))}," +
                    $"'{venda.dthVenda}')";
                try
                {
                    //Atualiza o estoque do produto
                    produto.qtdProduto = produto.qtdProduto - venda.qtdProdutoVendido;
                    ret = await _produtoRepository.Update(produto);

                    if (!ret.Sucesso) { return ret; }
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
            string query = "Use CRUDAngularC;Select * from Vendas V join Produtos P on V.idProduto = P.idProduto ";
            List<VendaModel> listProduto = new List<VendaModel>();
            ProdutoModel produtos;
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
                            qtdProdutoVendido = Convert.ToInt32(result["qtdProdutoVendido"].ToString()),
                            vlrTotal = float.Parse(result["vlrTotal"].ToString()),
                            dthVenda = DateTime.Parse(result["dthVenda"].ToString()),
                            Produto = new ProdutoModel
                            {
                                IdProduto = Convert.ToInt32(result["idProduto"]),
                                Nome = result["Nome"].ToString(),
                                Descricao = result["Descricao"].ToString(),
                                vlrProduto = float.Parse(result["vlrProduto"].ToString()),
                                qtdProduto = Convert.ToInt32(result["qtdProduto"]),
                                dthCriadoProduto = DateTime.Parse(result["dthCriacaoProduto"].ToString()),
                                dthAlteracaoProduto = DateTime.Parse(result["dthAlteraoProduto"].ToString()),
                                tpProduto = result["TpProduto"].ToString()
                            }
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
            string query = $"Use CRUDAngularC;Select * from Vendas where idVenda = {produtoBusca.IdProduto}";
            ProdutoModel produtos;
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
                            qtdProdutoVendido = Convert.ToInt32(result["qtdVendida"].ToString()),
                            dthVenda = DateTime.Parse(result["dthVenda"].ToString()),
                        };
                        produtos = new ProdutoModel { IdProduto = Convert.ToInt32(result["idProduto"]) };
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
