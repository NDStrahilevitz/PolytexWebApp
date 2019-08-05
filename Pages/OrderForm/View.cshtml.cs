using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PolytexWebApp.DataLayer;
using PolytexWebApp.Models;

namespace PolytexWebApp.Pages
{
    public class ViewOrderFormModel : PageModel
    {
        private readonly DeviceOrderRepository deviceRepository;
        private readonly PurchaseOrderRepository poRepository;
        private readonly IConfiguration config;

        public ViewOrderFormModel(IConfiguration _config){
            config = _config;
            string connectionString = config.GetConnectionString("PolytexContext");
            deviceRepository = new DeviceOrderRepository(connectionString);
            poRepository = new PurchaseOrderRepository(connectionString);
        }

        public PurchaseOrder order {get; private set;}
        public IEnumerable<DeviceOrder> devices {get; private set;}
        
        public async Task<IActionResult> OnGetAsync(ulong id){
            order = await poRepository.GetByID(id);
            if(order == null){
                return NotFound();
            }
            devices = await deviceRepository.GetAllByPO(order);
            return Page();
        }
    }
}