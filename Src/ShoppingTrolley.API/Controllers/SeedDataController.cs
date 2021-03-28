using Microsoft.AspNetCore.Mvc;
using ShoppingTrolley.Application.Commands.ShoppingCarts;
using ShoppingTrolley.Application.Queries.ShoppingCarts;
using ShoppingTrolley.Application.ViewModels;
using System.Threading.Tasks;

namespace ShoppingTrolley.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SeedDataController : BaseController
    {
        /// <summary>
        /// Seed Data to DB
        /// </summary>
        /// <param name="command">Seed data command.</param>
        /// <returns>seeded items</returns>
        /// <response code="200">Returns a json object containing the slist of product ids and customer ids</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The resource could not be found.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost]
        [Produces(typeof(SeedDataViewModel))]
        [ProducesResponseType(typeof(SeedDataViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SeedDataViewModel>> Seed([FromBody] SeedDataCommand command)
        {
            var shoppingCart = await Mediator.Send(command);

            return Ok(shoppingCart);
        }
    }
}
