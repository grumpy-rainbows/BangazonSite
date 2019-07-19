using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; }
        public List<Product> AvailableProductType { get; set; }

        // NOTE: Here we use an expression bodied, read-only property
        //       AND the ?. operator
        //       ...good times....
        public List<SelectListItem> AvailableOption 
        {
            get
            {
                if(AvailableProductType ==null)
                {
                    return null;
                }

                var apt = AvailableProductType?.Select(p => new SelectListItem(p.Title, p.ProductId.ToString())).ToList();
                apt.Insert(0, new SelectListItem("Select product", null));

                return apt;
            }
        }
    }
}
