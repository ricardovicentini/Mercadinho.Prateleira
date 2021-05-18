using FluentValidation.Results;
using MediatR;
using Mercadinho.Prateleira.API.Application.Categoria.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mercadinho.Prateleira.API.Application.Categoria.Command
{
    public class CreateCategoryCommand : IRequest<bool>
    {
        public string Descricao { get; set; }
        
        [JsonIgnore]
        public ValidationResult Validation { get; }
        public CreateCategoryCommand(string descricao)
        {
            Descricao = descricao;
            var validator = new CreateCategoryCommandValidator();
            Validation = validator.Validate(this);
        }
    }
}
