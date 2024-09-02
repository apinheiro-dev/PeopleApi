using System.ComponentModel.DataAnnotations;

namespace PeopleApi.Data.Dtos.Usuarios
{
    public class CreateUsuarioDto
    {
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        [Compare("Senha")]
        public string SenhaConfirmacao { get; set; }
        //[Required]
        //public char Role { get; set; }
    }
}
