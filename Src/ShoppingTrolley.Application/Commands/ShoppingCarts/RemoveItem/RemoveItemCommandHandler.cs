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
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, ShoppingCartViewModel>
    {
        private readonly IShoppingCartDbContext _context;
        private readonly IMapper _mapper;

        public RemoveItemCommandHandler(IShoppingCartDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShoppingCartViewModel> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
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

            shoppingCart.RemoveItem(product);

            await _context.SaveChangesAsync(cancellationToken);

            var viewModel = _mapper.Map<ShoppingCartViewModel>(shoppingCart);

            return viewModel;
        }
    }
}
