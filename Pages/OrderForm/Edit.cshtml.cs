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
        
        public EditOrderFormModel(IConfiguration _config){
            config = _config;
            string connectionString = config.GetConnectionString("PolytexContext");
            deviceRepository = new DeviceOrderRepository(connectionString);
            poRepository = new PurchaseOrderRepository(connectionString);
        }

        //original models
        public PurchaseOrder oOrder;
        public IEnumerable<DeviceOrder> oDevice;

        [BindProperty]
        public PurchaseOrder _order {get; set;}
        [BindProperty]
        public DeviceOrder _device {get; set;}


        public async Task<IActionResult> OnPostAsync(){
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            await poRepository.Update(_order);
            await deviceRepository.Update(_device);

            return RedirectToPage("./Index");
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