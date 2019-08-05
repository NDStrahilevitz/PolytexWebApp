using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PolytexWebApp.DataLayer;
using PolytexWebApp.Models;

namespace PolytexWebApp.Pages
{
    public class ReportsModel : PageModel
    {
        private readonly PurchaseOrderRepository poRepository;
        private readonly IConfiguration config;

        public ReportsModel(IConfiguration _config){
            config = _config;
            string connectionString = config.GetConnectionString("PolytexContext");
            poRepository = new PurchaseOrderRepository(connectionString);
        }

        public IEnumerable<PurchaseOrder> orders;
        [BindProperty(SupportsGet = true)]
        public string companySearch {get;set;}
        [BindProperty(SupportsGet = true)]
        public string statusSearch{get;set;}

        public async Task OnGetAsync(){
            orders = await poRepository.GetAll();
            if(!string.IsNullOrEmpty(companySearch)){
                orders = orders.Where(order => order.companyName.Contains(companySearch));
            }
            if(!string.IsNullOrEmpty(statusSearch)){
                orders = orders.Where(order => order.orderStatus.ToString().Contains(statusSearch));
            }
        }
    }
}

