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
    public class CalculateTotalPriceCommandHandler : IRequestHandler<CalculateTotalPriceCommand, TotalPriceViewModel>
    {
        private readonly IShoppingCartDbContext _context;
        private readonly IMapper _mapper;

        public CalculateTotalPriceCommandHandler(IShoppingCartDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TotalPriceViewModel> Handle(CalculateTotalPriceCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(x => x.ShoppingCartItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.ShoppingCartId == request.ShoppingCartId)
                .FirstOrDefaultAsync();

            if (shoppingCart == null)
            {
                throw new NotFoundException(nameof(ShoppingCart), request.ShoppingCartId);
            }

            shoppingCart.CalculateTotalPrice();

            await _context.SaveChangesAsync(cancellationToken);

            var viewModel = _mapper.Map<TotalPriceViewModel>(shoppingCart.TotalPrice);

            return viewModel;
        }
    }
}
