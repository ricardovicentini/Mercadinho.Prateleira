using MediatR;
using Mercadinho.Prateleira.Application.API.Application.Estoque.Queries;
using Mercadinho.Prateleira.Infrastructure.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mercadinho.Prateleira.Application.API.Application.Estoque.Handler
{
    public class ProdutoEstoqueQueryHandler : IRequestHandler<ProdutoEstoqueQuery, Domain.Estoque>
    {
        private readonly IGenericRepository<Domain.Estoque> _genericRepository;

        public ProdutoEstoqueQueryHandler(IGenericRepository<Domain.Estoque> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Domain.Estoque> Handle(ProdutoEstoqueQuery request, CancellationToken cancellationToken)
        {
            var estoque = await _genericRepository.GetAllAsync(
                    noTracking: true,
                    filter: x => x.ProdutoId == request.IdProduto, 
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return estoque.FirstOrDefault();
                
        }
    }
}
