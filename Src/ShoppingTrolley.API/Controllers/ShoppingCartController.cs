using Microsoft.AspNetCore.Mvc;
using ShoppingTrolley.Application.Commands.ShoppingCarts;
using ShoppingTrolley.Application.Queries.ShoppingCarts;
using ShoppingTrolley.Application.ViewModels;
using System.Threading.Tasks;

namespace ShoppingTrolley.API.Controllers
{
    public class ShoppingCartController : BaseController
    {
        /// <summary>
        /// Add items to a Customer's shopping cart
        /// </summary>
        /// <param name="command">Add item command (requires valid customerId and productId).</param>
        /// <returns>The updated shopping cart</returns>
        /// <response code="200">Returns a json object containing the shopping cart id, customer id, items added to the shopping cart and the total count of items</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The Customer or Product could not be found.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost]
        [Produces(typeof(ShoppingCartViewModel))]
        [ProducesResponseType(typeof(ShoppingCartViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("additem")]
        public async Task<ActionResult<ShoppingCartViewModel>> AddItem([FromBody] AddItemCommand command)
        {
            var shoppingCart = await Mediator.Send(command);

            return Ok(shoppingCart);
        }

        /// <summary>
        /// Remove items from a Customer's shopping cart
        /// </summary>
        /// <param name="command">Remove item command (requires valid customerId and productId).</param>
        /// <returns>The updated shopping cart</returns>
        /// <response code="200">Returns a json object containing the shopping cart id, customer id, and the total count of items</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The Customer or Product could not be found.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost]
        [Produces(typeof(ShoppingCartViewModel))]
        [ProducesResponseType(typeof(ShoppingCartViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("removeitem")]
        public async Task<ActionResult<ShoppingCartViewModel>> RemoveItem([FromBody] RemoveItemCommand command)
        {
            var shoppingCart = await Mediator.Send(command);

            return Ok(shoppingCart);
        }

        /// <summary>
        /// View shopping cart details
        /// </summary>
        /// <param name="command">View shopping cart detail command (requires shopping cart id).</param>
        /// <returns>Gets all the items in the shopping cart</returns>
        /// <response code="200">Returns a json object containing the shopping cart id, customer id, items added to the shopping cart</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The shopping cart cannot be found.</response>
        /// <response code="500">An error has occurred</response>
        [HttpGet]
        [Produces(typeof(ShoppingCartDetailViewModel))]
        [ProducesResponseType(typeof(ShoppingCartDetailViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("view/{id}")]
        public async Task<ActionResult<ShoppingCartDetailViewModel>> View(long id)
        {
            var shoppingCartDetails = await Mediator.Send(new ViewShoppingCartDetailQuery { ShoppingCartId = id });

            return Ok(shoppingCartDetails);
        }

        ///// <summary>
        ///// Calculate total price of the shopping cart items
        ///// </summary>
        ///// <param name="command">Calculate total price command (requires shopping cart id).</param>
        ///// <returns>Gets total price of the items in the shopping cart</returns>
        ///// <response code="200">Returns a json object containing the total price, shopping cart id</response>
        ///// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        ///// <response code="404">The shopping cart cannot be found.</response>
        ///// <response code="500">An error has occurred</response>
        //[HttpPost]
        //[Produces(typeof(TotalPriceViewModel))]
        //[ProducesResponseType(typeof(TotalPriceViewModel), 200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]
        //[Route("calculatetotalprice")]
        //public async Task<ActionResult<ShoppingCartViewModel>> CalculateTotalPrice([FromBody] CalculateTotalPriceCommand command)
        //{
        //    var totalPrice = await Mediator.Send(command);

        //    return Ok(totalPrice);
        //}
    }
}
