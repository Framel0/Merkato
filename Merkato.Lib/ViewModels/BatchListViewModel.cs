using  Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class BatchListViewModel
    {
        public int Id { get; set; }
        public String BatchNo { get; set; }
        public int ActivitiID { get; set; }
        public string ClientName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BatchListViewModel()
        {

        }

        public BatchListViewModel(MerkatoDbContext context)
        {

            loadLists(context);
        }

        public void loadLists(MerkatoDbContext context)
        {

        }
        public BatchListViewModel(MerkatoDbContext context, ClientRequest activity) : this(context)
        {

        }
    }
}
