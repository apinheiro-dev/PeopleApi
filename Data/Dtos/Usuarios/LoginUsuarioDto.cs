using System.ComponentModel.DataAnnotations;

namespace PeopleApi.Data.Dtos.Usuarios
{
    public class LoginUsuarioDto
    {
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
