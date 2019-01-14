using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Bank
    {
        public Bank()
        {
            BankBranch = new HashSet<BankBranch>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public short Active { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public ICollection<BankBranch> BankBranch { get; set; }
    }
}
