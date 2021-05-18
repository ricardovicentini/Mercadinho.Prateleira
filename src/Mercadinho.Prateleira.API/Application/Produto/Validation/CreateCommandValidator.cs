using FluentValidation;
using Mercadinho.Prateleira.API.Application.Produto.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercadinho.Prateleira.API.Application.Produto.Validation
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull()
                .NotEmpty()
                .MaximumLength(300);


            RuleFor(x => x.IdCategorias)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Length > 0);
        }
    }
}
