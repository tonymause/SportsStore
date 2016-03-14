namespace SportsStore.Domain.Concrete
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Entities;

    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext _context = new EFDbContext();
        public IEnumerable<Product> Products => _context.Products;
    }
}