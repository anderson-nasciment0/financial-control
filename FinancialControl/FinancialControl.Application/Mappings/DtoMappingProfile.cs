using AutoMapper;
using FinancialControl.Application.DTOs;
using FinancialControl.Domain.Entities;


namespace FinancialControl.Application.Mappings
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Pessoa, PessoaDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Transacao, TransacaoDTO>().ReverseMap();
        }
    }
}
