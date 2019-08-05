using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using PolytexWebApp.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace PolytexWebApp.DataLayer
{
    public class PurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        private string _connectionString;

        public PurchaseOrderRepository(string connectionString){
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAll(){
            
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var result = await conn.QueryAsync<PurchaseOrder>("_sp_GetAllPurchaseOrders", commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<PurchaseOrder> GetByID(ulong id){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_PoNumber", id);

                var result = await conn.QuerySingleOrDefaultAsync<PurchaseOrder>("_sp_GetPurchaseOrderByID", paras, commandType: CommandType.StoredProcedure);
                return result;
            }          
        }

        public async Task<bool> Create(PurchaseOrder order){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_PoNumber", order.poNumber);
                paras.Add("_CompanyPo", order.poReference);
                paras.Add("_CompanyName", order.companyName);
                paras.Add("_ContactName", order.contactName);
                paras.Add("_ContactEmail", order.contactEmail);
                paras.Add("_ContactPhone", order.contactPhone);
                paras.Add("_OrderDate", order.orderDate);
                paras.Add("_DeliveryDate", order.deliveryDate);
                paras.Add("_OrderStatus", order.orderStatus);

                var result = await conn.ExecuteAsync("_sp_AddPurchaseOrder", paras, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> Update(PurchaseOrder order){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_PoNumber", order.poNumber);
                paras.Add("_CompanyPo", order.poReference);
                paras.Add("_CompanyName", order.companyName);
                paras.Add("_ContactName", order.contactName);
                paras.Add("_ContactEmail", order.contactEmail);
                paras.Add("_ContactPhone", order.contactPhone);
                paras.Add("_DeliveryDate", order.deliveryDate);
                paras.Add("_OrderStatus", order.orderStatus);

                var result = await conn.ExecuteAsync("_sp_UpdatePurchaseOrder", paras, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> Delete(ulong id){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_PoNumber", id);

                var result = await conn.ExecuteAsync("_sp_DeletePurchaseOrderByID", paras, commandType: CommandType.StoredProcedure);
                return result>0;
            }      
        }
    }
}