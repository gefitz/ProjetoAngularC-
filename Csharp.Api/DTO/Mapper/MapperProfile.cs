using AutoMapper;
using Csharp.Api.Model;

namespace Csharp.Api.DTO.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProdutoDTO,ProdutoModel>().ReverseMap();
            CreateMap<VendaDTO,VendaModel>().ReverseMap();
            CreateMap<TipoProdutoDTO,TipoProdutoModel>().ReverseMap();
        }
    }
}
