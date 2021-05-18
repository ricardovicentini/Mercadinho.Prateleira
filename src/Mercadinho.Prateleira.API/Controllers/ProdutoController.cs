using MediatR;
using Mercadinho.Prateleira.API.Application.Produto.Command;
using Mercadinho.Prateleira.API.Application.Produto.Query;
using Mercadinho.Prateleira.API.Application.Produto.Validation;
using Mercadinho.Prateleira.Application.API.Application.Produto.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mercadinho.Prateleira.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Obter(CancellationToken cancellationToken)
        {
            var produtos = await _mediator.Send(new ListaProdutosQuery(), cancellationToken)
                .ConfigureAwait(false);
            return produtos.Any() ?  Ok(produtos) : NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateCommand createCommand, 
            CancellationToken cancellationToken)
        {
            
            if (!createCommand.Validation.IsValid)
                return BadRequest(createCommand.Validation.Errors);

            var sucesso = await _mediator.Send(createCommand, cancellationToken)
                .ConfigureAwait(false);
            
            return Ok(sucesso);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateCommand updateCommand, CancellationToken cancellationToken)
        {
            var sucesso = await _mediator.Send(updateCommand, cancellationToken).ConfigureAwait(false);
            return sucesso ? Ok(true) : BadRequest();
        }

        //[HttpDelete]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Delelete(DeleteCommand deleteCommand, CancellationToken cancellationToken)
        //{
        //    var sucesso = await _mediator.Send(deleteCommand, cancellationToken).ConfigureAwait(false);
        //    return sucesso ? Ok(true) : BadRequest();
        //}
    }
}
