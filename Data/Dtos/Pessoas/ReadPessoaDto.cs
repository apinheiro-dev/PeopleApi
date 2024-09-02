namespace PeopleApi.Data.Dtos.Pessoas
{
    public class ReadPessoaDto
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Foto { get; set; }
        public string FotoAnterior { get; set; }
    }
}
