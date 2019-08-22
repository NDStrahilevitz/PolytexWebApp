using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PolytexWebApp.DataLayer;
using PolytexWebApp.Models;


namespace PolytexWebApp.Pages.OrderForm
{
    public class EditOrderFormModel : PageModel
    {
        private readonly DeviceOrderRepository deviceRepository;
        private readonly PurchaseOrderRepository poRepository;
        private readonly IConfiguration config;
        
        //original models
        public PurchaseOrder oOrder {get; private set;}
        public IEnumerable<DeviceOrder> oDevice {get; private set;}

        public EditOrderFormModel(IConfiguration _config){
            config = _config;
            string connectionString = config.GetConnectionString("PolytexContext");
            deviceRepository = new DeviceOrderRepository(connectionString);
            poRepository = new PurchaseOrderRepository(connectionString);
        }

        [BindProperty]
        public PurchaseOrder _order {get; set;}
        [BindProperty]
        public DeviceOrder _device {get; set;}

        public async Task<IActionResult> OnPostAsync(ulong id){
            
            if(!ModelState.IsValid){
                return Page();
            }

            _order.poNumber = id;

            await poRepository.Update(_order);
            await deviceRepository.Update(_device);

            return Redirect("~/OrderForm/View/" + id);
        }

        public async Task<IActionResult> OnGetAsync(ulong id){
            oOrder = await poRepository.GetByID(id);
            if(oOrder == null){
                return NotFound();
            }
            oDevice = await deviceRepository.GetAllByPO(oOrder);

            return Page();
        }
    }
}