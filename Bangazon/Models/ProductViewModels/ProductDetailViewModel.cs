using Bangazon.Models;
using Bangazon.Data;
using System.Collections.Generic;

namespace Bangazon.Models.ProductViewModels
{
  public class ProductDetailViewModel
  {
        internal List<OrderProduct> orderProducts;

        public Product Product { get; set; }
  }
}