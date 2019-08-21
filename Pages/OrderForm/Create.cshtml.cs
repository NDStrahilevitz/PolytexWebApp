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
    public class CreateOrderFormModel : PageModel
    {
        private readonly DeviceOrderRepository deviceRepository;
        private readonly PurchaseOrderRepository poRepository;
        private readonly DailyCounterRepository counterRepository;
        private readonly IConfiguration config;

        public string poNumSt {get; private set;}
        public ulong poNum {get; private set;}
        public DateTime orderDate{get; private set;}
        
        public CreateOrderFormModel(IConfiguration _config){
            config = _config;
            string connectionString = config.GetConnectionString("PolytexContext");
            deviceRepository = new DeviceOrderRepository(connectionString);
            poRepository = new PurchaseOrderRepository(connectionString);
            counterRepository = new DailyCounterRepository(connectionString);
        }
  
        [BindProperty]
        public PurchaseOrder _order {get; set;}
        [BindProperty]
        public DeviceOrder _device {get; set;}

        public async Task OnGetAsync(){
            await InitData();
        }

        public async Task<IActionResult> OnPostAsync(){
            await InitData();
            
            if(!ModelState.IsValid){
                return Page();
            }

            _order.poNumber = poNum;
            _order.orderDate = orderDate;

            _device.poNumber = poNum;

            await poRepository.Create(_order);
            await deviceRepository.Create(_device);

            return RedirectToPage("/View/" + poNumSt);
        }

        public async Task<ulong> GetCounter(){
            return await counterRepository.GetCounter();
        }

        public async Task InitData(){
            ulong counter = await GetCounter();
            orderDate = DateTime.Now.Date;
            poNumSt = ulong.Parse(orderDate.ToString("yyyyMMdd")).ToString() + counter.ToString();
            poNum = UInt64.Parse(poNumSt);
        }
    }
}