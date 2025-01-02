using AutoMapper;
using Csharp.Api.DTO;
using Csharp.Api.Model;
using Csharp.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Csharp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : Controller
    {
        private readonly IMapper _mapper;
        private ReturnModel ret = new ReturnModel();
        private readonly VendaRepository _repository;

        public VendaController(IMapper mapper, VendaRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        [HttpPost]
        public async Task<ActionResult<ReturnModel>> Create(List<VendaDTO> venda)
        {
            if (venda == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar produto";
                return BadRequest(ret);
            }
            var model = _mapper.Map<List<VendaModel>>(venda);

            ret = await _repository.Create(model);

            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }

        }
        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<ReturnModel>> SelectAll()
        {
            ret = await _repository.SelectAll();
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
        [HttpGet("buscaProduto")]
        public async Task<ActionResult<ReturnModel>> SelectProdutoBy(VendaDTO venda)
        {
            if (venda == null)
            {
                ret.Sucesso = false;
                ret.Mensagem = "Deve passar parametro de busca";
            }
            var model = _mapper.Map<VendaModel>(venda);
            ret = await _repository.SelectBy(model);
            if (ret.Sucesso) { return Ok(ret); }
            else { return BadRequest(ret); }
        }
    }
}
