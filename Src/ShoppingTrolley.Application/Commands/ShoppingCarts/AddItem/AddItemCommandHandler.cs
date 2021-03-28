using AutoMapper;
using MediatR;
using ShoppingTrolley.Application.Common.Exceptions;
using ShoppingTrolley.Application.Common.Interfaces;
using ShoppingTrolley.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ShoppingTrolley.Application.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ShoppingTrolley.Application.Commands.ShoppingCarts
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, ShoppingCartViewModel>
    {
        private readonly IShoppingCartDbContext _context;
        private readonly IMapper _mapper;

        public AddItemCommandHandler(IShoppingCartDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShoppingCartViewModel> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), request.CustomerId);
            }

            var shoppingCart = await _context.ShoppingCarts
                .Include(x => x.ShoppingCartItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.CustomerId == request.CustomerId)
                .FirstOrDefaultAsync();

            if (shoppingCart == null)
            {
                // Add new shopping cart
                customer.ShoppingCart = new ShoppingCart
                {
                    CustomerId = customer.CustomerId
                };

                shoppingCart = customer.ShoppingCart;
            }

            var product = await _context.Products
                .Where(x => x.ProductId == request.ProductId)
                .FirstOrDefaultAsync();

            shoppingCart.AddItem(product);

            //var item = shoppingCart.ShoppingCartItems
            //    .Where(item => item.Product.ProductId == request.ProductId)
            //    .FirstOrDefault();

            //if (item == null)
            //{
                
            //    // Add new item
            //    ShoppingCartItem newItem = new ShoppingCartItem
            //    {
            //        Product = product,
            //        Quantity = 1
            //    };

            //    shoppingCart.ShoppingCartItems.Add(newItem);
            //}
            //else
            //{
            //    //update Quantity
            //    item.Quantity += 1;
            //}

            //// update the cart items count
            //shoppingCart.ItemsCount += 1;


            await _context.SaveChangesAsync(cancellationToken);

            var viewModel = _mapper.Map<ShoppingCartViewModel>(shoppingCart);

            return viewModel;
        }
    }
}
