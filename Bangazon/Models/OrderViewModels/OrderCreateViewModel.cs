using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bangazon.Models.OrderViewModels
{
    public class OrderCreateViewModel
    {
        public Order Order { get; set; }
        public List<Product> AvailableProducts { get; set; }

        // NOTE: Here we use an expression bodied, read-only property
        //       AND the ?. operator
        //       ...good times....
        public List<SelectListItem> ProductOptions =>
            AvailableProducts?.Select(p => new SelectListItem(p.Title, p.ProductId.ToString())).ToList();
    }
}
