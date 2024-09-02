using FluentValidation;
using DocumentValidator;
using PeopleApi.Data.Dtos.Pessoas;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CheckData;

namespace PeopleApi.Validators
{
    public class CreatePessoaValidator : AbstractValidator<CreatePessoaDto>
    {
        IMain main = new Main();
        public CreatePessoaValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome da pessoa deve ser informado")
                .Length(3, 20).WithMessage($"O nome deve possuir entre {3} e {20} caracteres");

            RuleFor(p => p.SobreNome)
                .NotEmpty().WithMessage("O sobrenome da pessoa deve ser informado")
                .Length(3, 100).WithMessage($"O nome deve possuir entre {3} e {100} caracteres");

            // CPF validado com e sem formatação. Ex.: 225.536.200-71 ou 70061435007
            RuleFor(p => CpfValidation.Validate(p.Cpf)).Equal(true)
                .WithMessage("O CPF fornecido é inválido");

            RuleFor(p => p.DataNascimento)
                .Must(data => main.IsDate(data.ToString()) ).WithMessage("É necessário informar uma data válida")
                .NotEmpty().WithMessage("Informe a data de nascimento da pessoa")
                .ExclusiveBetween(new DateTime(1900, 01, 01), DateTime.Now.AddDays(-1))
                    .WithMessage("Deve ser maior que 01/01/1900 e menor que a data atual");

            RuleFor(p => p.Sexo)
                .NotEmpty().WithMessage("Informe o sexo da pessoa")
                .Must(sexo => sexo.ToUpper() == "M" || sexo.ToUpper() == "F")
                    .WithMessage("Informe apenas 1 caracter para o sexo da pessoa: M - masculino | F - feminino");

            RuleFor(p => p.Foto)
                .NotEmpty().WithMessage("Insira uma foto da pessoa")
                .Must(foto => foto.Length <= 1000).WithMessage("Foto da pessoa deve possuir no máximo 1000")
                .LessThanOrEqualTo("1Mb").WithMessage("Foto da pessoa deve possuir no máximo 1Mb")
                ;

            //RuleFor(p => p.FotoAnterior)
            //    // Escrever
            //    ;
        }
    }
}
