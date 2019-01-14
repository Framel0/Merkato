using  Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class ClientProductViewModel:ClientProduct
    {
        public string ClientName { get; set; }

        public ClientProductViewModel()
        {

        }

        public ClientProductViewModel(MerkatoDbContext context)
        {
            Id = 0;
        }

        public ClientProductViewModel(MerkatoDbContext context, ClientProduct product):this(context)
        {
            this.Id = product.Id;
            this.ClientId = product.ClientId;
            this.ProductName = product.ProductName;
        }

        public ClientProduct GetModel()
        {
            ClientProduct a = new ClientProduct();

            a.Id = this.Id;
            a.ClientId = this.ClientId;
            a.ProductName = this.ProductName;

            return a;
        }
    }
}
