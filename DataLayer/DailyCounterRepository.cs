using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace PolytexWebApp.DataLayer
{
    public class DailyCounterRepository
    {
        private string _connectionString;
        public DailyCounterRepository(string connectionString){
            _connectionString = connectionString;
        }

        public async Task<ulong> GetCounter(){
            using(var conn = new MySqlConnection(_connectionString)){
                var query = "SELECT counter FROM daily_counter WHERE id = 0";

                return await conn.QueryFirstOrDefaultAsync<ulong>(query);
            }
        }
    }
}