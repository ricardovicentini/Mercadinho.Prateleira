using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercadinho.Prateleira.API.Application.Categoria.Query
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Domain.Categoria>>
    {
    }
}
