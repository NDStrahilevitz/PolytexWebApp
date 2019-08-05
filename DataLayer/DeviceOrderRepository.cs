using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using PolytexWebApp.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace PolytexWebApp.DataLayer
{
    public class DeviceOrderRepository : IRepository<DeviceOrder>
    {
        private string _connectionString;

        public DeviceOrderRepository(string connectionString){
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DeviceOrder>> GetAll(){
            
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                string query = "SELECT * FROM device_orders";
                var result = await conn.QueryAsync<DeviceOrder>(query);

                return result;
            }
        }

        public async Task<DeviceOrder> GetByID(ulong id){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_DeviceNumber", id);

                var result = await conn.QuerySingleOrDefaultAsync<DeviceOrder>("_sp_GetDeviceOrderByID", paras, commandType: CommandType.StoredProcedure);
                return result;
            }          
        }

        public async Task<IEnumerable<DeviceOrder>> GetAllByPO(PurchaseOrder order){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_PoNumber", order.poNumber);

                var result = await conn.QueryAsync<DeviceOrder>("_sp_GetDeviceOrdersByPO", paras, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> Create(DeviceOrder order){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_DeviceNumber", order.deviceNumber);
                paras.Add("_Model", order.deviceModel);
                paras.Add("_PoNumber", order.poNumber);
                paras.Add("_Voltage", order.voltage);
                paras.Add("_Compressor", order.compressor);
                paras.Add("_IdDevice", order.idDevice);
                paras.Add("_CardReader", order.cardReader);
                paras.Add("_Cells", order.cells);

                var result = await conn.ExecuteAsync("_sp_AddDeviceOrder", paras, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> Update(DeviceOrder order){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_DeviceNumber", order.deviceNumber);
                paras.Add("_Model", order.deviceModel);
                paras.Add("_Voltage", order.voltage);
                paras.Add("_Compressor", order.compressor);
                paras.Add("_IdDevice", order.idDevice);
                paras.Add("_CardReader", order.cardReader);
                paras.Add("_Cells", order.cells);

                var result = await conn.ExecuteAsync("_sp_UpdateDeviceOrder", paras, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
        
        public async Task<bool> Delete(ulong id){
            using(var conn = new MySqlConnection(_connectionString)){
                await conn.OpenAsync();

                var paras = new DynamicParameters();
                paras.Add("_DeviceNumber", id);

                var result = await conn.ExecuteAsync("_sp_DeleteDeviceOrderByID", paras, commandType: CommandType.StoredProcedure);
                return result>0;
            }      
        }
    }
}