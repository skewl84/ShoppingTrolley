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

namespace ShoppingTrolley.Application.Queries.ShoppingCarts
{
    public class ViewShoppingCartDetailQueryHandler : IRequestHandler<ViewShoppingCartDetailQuery, ShoppingCartDetailViewModel>
    {
        private readonly IShoppingCartDbContext _context;
        private readonly IMapper _mapper;

        public ViewShoppingCartDetailQueryHandler(IShoppingCartDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShoppingCartDetailViewModel> Handle(ViewShoppingCartDetailQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(x => x.ShoppingCartItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductPromotion)
                .Where(x => x.ShoppingCartId == request.ShoppingCartId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (shoppingCart == null)
            {
                throw new NotFoundException(nameof(ShoppingCart), request.ShoppingCartId);
            }

            var viewModel = _mapper.Map<ShoppingCartDetailViewModel>(shoppingCart);

            return viewModel;
        }
    }
}
