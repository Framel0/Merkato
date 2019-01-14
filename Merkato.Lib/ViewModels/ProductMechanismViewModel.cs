using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class ProductMechanismViewModel: ProductMechanism
    {
        public string ProductName { get; set; }
        public string MechanismName { get; set; }

        public List<SelectListItem> ProductList { get; set; }
        public List<SelectListItem> MechanismList { get; set; }

        public ProductMechanismViewModel()
        {

        }

        public ProductMechanismViewModel(MerkatoDbContext context)
        {
            ProductList = new List<SelectListItem>();
            MechanismList = new List<SelectListItem>();

            loadLists(context);
        }

        public void loadLists(MerkatoDbContext context)
        {
            ProductList = context.ClientProduct.Select(p => new SelectListItem() { Text = p.ProductName, Value = p.Id.ToString() }).ToList();
            MechanismList = context.Mechanism.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public ProductMechanismViewModel(MerkatoDbContext context, ProductMechanism product) : this(context)
        {
            this.Id = product.Id;
            this.MechanismId = product.MechanismId;
            this.ProductId = product.ProductId;
            this.ProductOrder = product.ProductOrder;
            this.Quantity = product.Quantity;
        }

        public ProductMechanism GetModel()
        {
            ProductMechanism a = new ProductMechanism();

            a.Id = this.Id;
            a.MechanismId = this.MechanismId;
            a.ProductId = this.ProductId;
            a.ProductOrder = this.ProductOrder;
            a.Quantity = this.Quantity;

            return a;
        }
    }
}
