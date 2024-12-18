using AutoMapper;
using Csharp.Api.DTO;
using Csharp.Api.Model;
using Csharp.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Csharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoProdutoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly TpProdutoRepository _repository;
        private ReturnModel ret = new ReturnModel();
        public TipoProdutoController(IMapper mapper, TpProdutoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        [HttpPost]
        public async Task<ActionResult<ReturnModel>> Create(TipoProdutoDTO tpProduto)
        {
            if (tpProduto == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar tpProduto";
                return BadRequest(ret);
            }
            var model = _mapper.Map<TipoProdutoModel>(tpProduto);
            ret = await _repository.Create(model);
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }

        }
        [HttpGet]
        public async Task<ActionResult<ReturnModel>> SelectAll()
        {
            ret = await _repository.SelectAll();
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
        [HttpGet("buscaTipoProduto")]
        public async Task<ActionResult<ReturnModel>> SelectProdutoBy(TipoProdutoDTO tpProduto)
        {
            if(tpProduto == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar parametro de busca";
            }
            var model = _mapper.Map<ProdutoModel>(tpProduto);
            ret = await _repository.SelectBy(model);
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
        [HttpDelete]
        public async Task<ActionResult<ReturnModel>> Delete(TipoProdutoDTO tpProduto)
        {
            if (tpProduto == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar parametro para deletar";
            }
            var model = _mapper.Map<ProdutoModel>(tpProduto);
            ret = await _repository.Delete(model);
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
        [HttpPut]
        public async Task<ActionResult<ReturnModel>> Update(TipoProdutoDTO tpProduto)
        {
            if (tpProduto == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar parametro de Update";
            }
            var model = _mapper.Map<ProdutoModel>(tpProduto);
            ret = await _repository.Update(model);
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
    }
}

