using MediatR;
using Mercadinho.Prateleira.API.Application.Categoria.Command;
using Mercadinho.Prateleira.API.Application.Categoria.Query;
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
    public class CategoriaController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
        {
            var categorias = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken)
                .ConfigureAwait(false);

            return categorias.Any() ? Ok(categorias) : NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateCategoryCommand createCategoryCommand,
            CancellationToken cancellationToken)
        {
            if (!createCategoryCommand.Validation.IsValid)
                return BadRequest(createCategoryCommand.Validation.Errors);

            var sucesso = await _mediator.Send(createCategoryCommand, cancellationToken)
                .ConfigureAwait(false);

            return Ok(sucesso);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateCategoryCommand updateCategoryCommand, 
            CancellationToken cancellationToken)
        {
            var sucesso = await _mediator.Send(updateCategoryCommand, cancellationToken)
                .ConfigureAwait(false);

            return Ok(sucesso);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteCategoryCommand deleteCategoryCommand, 
            CancellationToken cancellationToken)
        {
            var sucesso = await _mediator.Send(deleteCategoryCommand, cancellationToken)
                .ConfigureAwait(false);

            return Ok(sucesso);
        }
    }
}
