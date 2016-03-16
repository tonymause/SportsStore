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

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var entity = _context.Products.Find(product.ProductID);
                if (entity != null)
                {
                    entity.Name = product.Name;
                    entity.Description = product.Description;
                    entity.Price = product.Price;
                    entity.Category = product.Category;
                    entity.ImageData = product.ImageData;
                    entity.ImageMimeType = product.ImageMimeType;
                }
            }

            _context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            var entity = _context.Products.Find(productId);
            if (entity != null)
            {
                _context.Products.Remove(entity);
                _context.SaveChanges();
            }
            return entity;
        }
    }
}