using System.ComponentModel.DataAnnotations;

namespace PeopleApi.Models
{
    public class Pessoa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        //[Required(ErrorMessage = "O nome da pessoa é obrigatório")]
                    //[MinLength(3, ErrorMessage = "O nome da pessoa não pode possuir menos de 3 caracteres")]
                    //[MaxLength(20, ErrorMessage = "O nome da pessoa não pode exceder 20 caracteres")]
                    //[StringLength(3, 20, ErrorMessage = "O nome da pessoa deve possuir mínimo de 3 e máximo de 20 caracteres")]
        //[StringLength(20, ErrorMessage = "{0} O nome deve possuir entre {2} e {1} caracteres", MinimumLength = 3)]
        //A mensagem de erro criada pelo código anterior seria "O comprimento do endereço deve estar entre 6 e 8".
        public string Nome { get; set; }
        //[Required(ErrorMessage = "O nome da pessoa é obrigatório")]
        //[StringLength(100, ErrorMessage = "{0} O sobrenome deve possuir entre {2} e {1} caracteres", MinimumLength = 3)]
        //A mensagem de erro criada pelo código anterior seria "O comprimento do endereço deve estar entre 6 e 8".
        public string SobreNome { get; set; }
        [Required]
        //[Range(11, 11, ErrorMessage = "O CPF deve possuir 11 dígitos")]
        public string Cpf { get; set; }
        //[Required]
            // Validação: Ser maior que 01/01/1900 e Menor que a Data ATual
        public DateTime DataNascimento { get; set; }
        [Required]
        // Validar Sexo M ou F
        public string Sexo { get; set; }
        [Required]
        // Validar Foto
        // Aceitar qualquer formato mas salvar no banco como .Jpeg e Máx 1Mb
        public string Foto {  get; set; }
        public string FotoAnterior { get; set; }        
    }
}
