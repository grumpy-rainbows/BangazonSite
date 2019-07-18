using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Data;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductListViewModel
    {
        public ProductType ProductType { get; set; }
        public int Count { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}