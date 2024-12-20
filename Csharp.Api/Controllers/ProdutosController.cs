using AutoMapper;
using Csharp.Api.DTO;
using Csharp.Api.Model;
using Csharp.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Csharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : Controller 
    {
        private readonly ProdutoRepository _repository;
        private ReturnModel ret = new ReturnModel();
        private readonly IMapper _mapper;

        public ProdutosController(ProdutoRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<ReturnModel>> Create (ProdutoDTO produto)
        {
            if (produto == null) 
            { 
                ret.Sucesso = false; 
                ret.Mensagem = "Deve passar produto"; 
                return BadRequest(ret); 
            }
            var model = _mapper.Map<ProdutoModel>(produto);

            ret = await _repository.Create(model);

            if(ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }

        }
        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<ReturnModel>> SelectAll()
        {
            ret = await _repository.SelectAll();
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
        [HttpPost("buscaProduto")]
        public async Task<ActionResult<ReturnModel>> SelectProdutoBy(ProdutoDTO produto)
        {
            if (produto == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar parametro de busca";
            }
            var model = _mapper.Map<ProdutoModel>(produto);
            ret = await _repository.SelectBy(model);
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
        [HttpDelete]
        public async Task<ActionResult<ReturnModel>> Delete(ProdutoDTO produto)
        {
            if (produto == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar parametro para deletar";
            }
            var model = _mapper.Map<ProdutoModel>(produto);
            ret = await _repository.Delete(model);
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
        [HttpPut]
        public async Task<ActionResult<ReturnModel>> Update(ProdutoDTO produto)
        {
            if (produto == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar parametro de Update";
            }
            var model = _mapper.Map<ProdutoModel>(produto);
            ret = await _repository.Update(model);
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }

    }
}
