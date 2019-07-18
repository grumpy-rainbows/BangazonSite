using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models
{
    public class ProductTypesViewModel
    {
        public ProductType ProductType { get; set; }
        public int Count { get; set; }
        public List<GroupedProducts> GroupedProducts { get; set; }
    }
}
