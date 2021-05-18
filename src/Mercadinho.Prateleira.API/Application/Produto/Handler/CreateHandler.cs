using MediatR;
using Mercadinho.Prateleira.API.Application.Produto.Command;
using Mercadinho.Prateleira.Infrastructure.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mercadinho.Prateleira.API.Application.Produto.Handler
{
    public class CreateHandler : IRequestHandler<CreateCommand, bool>
    {
        private readonly IGenericRepository<Domain.Produto> _produtoRepository;
        private readonly IGenericRepository<Domain.Categoria> _categoiaRepository;

        public CreateHandler(IGenericRepository<Domain.Produto> produtoRepository, 
            IGenericRepository<Domain.Categoria> categoiaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoiaRepository = categoiaRepository;
        }

        public async Task<bool> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
         
            var categorias = _categoiaRepository.GetAll()
                .Where(x => request.IdCategorias.Contains(x.Id)).ToList();


            var produto = new Domain.Produto
            {
                Descricao = request.Descricao,
                Categorias = categorias
            };
            await _produtoRepository.AddAsync(produto, cancellationToken).ConfigureAwait(false);
            return await _produtoRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
