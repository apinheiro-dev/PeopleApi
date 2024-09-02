using AutoMapper;
using PeopleApi.Models;
using PeopleApi.Data.Dtos.Pessoas;

namespace PeopleApi.Profiles
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<CreatePessoaDto, Pessoa>();
            CreateMap<UpdatePessoaDto, Pessoa>();
            CreateMap<Pessoa, UpdatePessoaDto>();
            CreateMap<Pessoa, ReadPessoaDto>()
                .ForMember(pessoaDto => pessoaDto.Nome,
                        opt => opt.MapFrom(pessoa => pessoa.Nome));
        }
    }
}
