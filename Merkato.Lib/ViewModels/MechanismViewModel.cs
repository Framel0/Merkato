using  Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class MechanismViewModel:Mechanism
    {
        public string ClientName { get; set; }

        public string ProductName { get; set; }

        public MechanismViewModel()
        {

        }

        public MechanismViewModel(MerkatoDbContext context)
        {
            Id = 0;
        }

        public MechanismViewModel(MerkatoDbContext context, Mechanism mechanism) : this(context)
        {
            this.Id = mechanism.Id;
            this.ClientId = mechanism.ClientId;
            this.Name = mechanism.Name;
            this.Price = mechanism.Price;
        }

        public Mechanism GetModel()
        {
            Mechanism a = new Mechanism();

            a.Id = this.Id;
            a.ClientId = this.ClientId;
            a.Name = this.Name;
            a.Price = this.Price;

            return a;
        }
    }
}
