using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class ClientViewModel : Client
    {
        public List<SelectListItem> ActiveList { get; set; }

        public List<ClientProductViewModel> Products { get; set; }
        public string ActiveString { get; set; }
        public ClientViewModel()
        {

        }


        public ClientViewModel(MerkatoDbContext context)
        {
            ActiveList = new List<SelectListItem>();

            loadLists(context);

            Products = new List<ClientProductViewModel>();

            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {
            ActiveList = context.ActiveList.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            //Products = (from cp in context.ClientProduct
            //            join p in context.Product on cp.ProductId equals p.Id
            //            join u in context.Unit on p.UnitId.Value equals u.Id
            //            where cp.ClientId == this.Id
            //            select new ClientProductViewModel()
            //            {
            //                ClientId = cp.ClientId,
            //                ProductId = cp.ProductId,
            //                Price = cp.Price,
            //                ProductCode = p.Code,
            //                ProductName = p.Name,
            //                ProductUnit = u.Name,
            //                Quantity = cp.Quantity

            //            }).ToList();

            //it should work lets try to show it int alist
            //where ist the view?

        }
        public ClientViewModel(MerkatoDbContext context, Client client) : this(context)
        {
            this.Id = client.Id;
            this.ClientCode = client.ClientCode;
            this.ClientName = client.ClientName;
            this.Address = client.Address;
            this.Email = client.Email;
            this.PhoneNumber = client.PhoneNumber;
            this.FirstContactName = client.FirstContactName;
            this.FirstContactPhone = client.FirstContactPhone;
            this.FirstContactEmail = client.FirstContactEmail;
            this.SecondContactName = client.SecondContactName;
            this.SecondContactPhone = client.SecondContactPhone;
            this.SecondContactEmail = client.SecondContactEmail;
            this.ClientAppUserName = client.ClientAppUserName;
            this.ClientAppPassword = client.ClientAppPassword;
            this.Status = client.Status;
        }

        public Client GetModel()
        {
            Client c = new Client();
            c.Id = this.Id;
            c.ClientCode = this.ClientCode;
            c.ClientName = this.ClientName;
            c.Address = this.Address;
            c.Email = this.Email;
            c.PhoneNumber = this.PhoneNumber;
            c.FirstContactName = this.FirstContactName;
            c.FirstContactPhone = this.FirstContactPhone;
            c.FirstContactEmail = this.FirstContactEmail;
            c.SecondContactName = this.SecondContactName;
            c.SecondContactPhone = this.SecondContactPhone;
            c.SecondContactEmail = this.SecondContactEmail;
            c.ClientAppUserName = this.ClientAppUserName;
            c.ClientAppPassword = this.ClientAppPassword;
            c.Status = this.Status;

            return c;
        }
    }
}
