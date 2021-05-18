using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercadinho.Prateleira.Application.API.Application.Estoque.Queries
{
    public class ProdutoEstoqueQuery : IRequest<Domain.Estoque>
    {
        public int IdProduto { get; set; }
    }
}
