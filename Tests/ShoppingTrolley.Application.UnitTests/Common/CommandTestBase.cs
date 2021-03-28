using ShoppingTrolley.Infrastructure.Persistence;
using System;

namespace ShoppingTrolley.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ShoppingCartDbContext _context;

        public CommandTestBase()
        {
            _context = ShoppingCartDbContextFactory.Create();
        }

        public void Dispose()
        {
            ShoppingCartDbContextFactory.Destroy(_context);
        }
    }
}
