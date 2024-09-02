using AutoMapper;
using PeopleApi.Data.Dtos.Usuarios;
using PeopleApi.Models;

namespace PeopleApi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>()
                .ForMember(usuarioDto => usuarioDto.UserName,
                        opt => opt.MapFrom(usuario => usuario.NomeUsuario));

            /*
             .ForMember(pessoaDto => pessoaDto.Nome,
                        opt => opt.MapFrom(pessoa => pessoa.Nome));
             */
        }
    }
}
