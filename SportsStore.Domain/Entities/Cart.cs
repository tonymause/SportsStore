using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            // find the cartline
            var line = _lineCollection.FirstOrDefault(x => x.Product.ProductID == product.ProductID);
            if (line == null)
            {
                _lineCollection.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            _lineCollection.RemoveAll(x => x.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines => _lineCollection;
    }
}