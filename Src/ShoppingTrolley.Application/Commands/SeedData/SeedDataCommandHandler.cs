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
using System;
using System.Collections.Generic;
using ShoppingTrolley.Domain.Entities.Promotion;
using ShoppingTrolley.Domain.Enums;

namespace ShoppingTrolley.Application.Commands.ShoppingCarts
{
    public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand, SeedDataViewModel>
    {
        private readonly IShoppingCartDbContext _context;
        private readonly IMapper _mapper;

        public SeedDataCommandHandler(IShoppingCartDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SeedDataViewModel> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            ResetData();
            SeedData();

            await _context.SaveChangesAsync(cancellationToken);

            var products = _context.Products.Select(m => m.ProductId).ToList();
            var customers = _context.Customers.Select(m => m.CustomerId).ToList();

            var viewModel = new SeedDataViewModel
            {
                CustomerIds = customers,
                ProductIds = products
            };

            return viewModel;
        }

        private void ResetData()
        {
            var customers = _context.Customers.ToArray();
            if (customers != null && customers.Length > 0)
            {
                _context.Customers.RemoveRange(customers);
            }

            var products = _context.Products.ToArray();
            if (products != null && products.Length > 0)
            {
                _context.Products.RemoveRange(products);
            }
        }

        private void SeedData()
        {
            // Add products
            AddProducts();
            // Add customers
            AddCustomers();
        }

        private void AddCustomers()
        {
            int count = 1;
            while (count <= 5)
            {
                _context.Customers.Add(new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    CustomerFirstName = "Test",
                    CustomerLastName = "User " + count
                });

                count++;
            }
        }

        private void AddProducts()
        {
            _context.Products.Add(new Product
            {
                ProductId = 1,
                ProductName = "Victoria Bitter",
                ProductSalePrice = 21.49,
                ProductPromotion = new ProductPromotion
                {
                    ProductPromotionDefinition = ProductPromotionDefinition.TwoDollarsOff,
                    PromotionName = "Two dollars off"
                }
            });
            _context.Products.Add(new Product
            {
                ProductId = 2,
                ProductName = "Crown Lager",
                ProductSalePrice = 22.99
            });
            _context.Products.Add(new Product
            {
                ProductId = 3,
                ProductName = "Coopers",
                ProductSalePrice = 20.49
            });
            _context.Products.Add(new Product
            {
                ProductId = 4,
                ProductName = "Tooheys Extra Dry",
                ProductSalePrice = 19.99
            });
        }
    }
}
