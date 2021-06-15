

using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MidasoftTest.Data.DataContext;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MidasoftTest.Data.Repositories.Interface
{
    public class FamilyGroupRepository : IFamilyGroupRepository
    {
        private readonly string _sqlConnectionString;
        public FamilyGroupRepository(IConfiguration configuration)
        {
            _sqlConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<FamilyGroup>> GetAllDapperAsync()
        {
                using (IDbConnection conn = new SqlConnection(_sqlConnectionString))
                {
                    return await conn.QueryAsync<FamilyGroup>(
                        "uspGetAll",
                        commandType: CommandType.StoredProcedure
                    );
                }            
        }
        public async Task<FamilyGroup> GetByUserDapperName(string userName)
        {
                using (IDbConnection conn = new SqlConnection(_sqlConnectionString))
                {
                    return await conn.QuerySingleOrDefaultAsync<FamilyGroup>(
                        "uspGetByUserName",
                        new { UserName = userName },
                        commandType: CommandType.StoredProcedure
                        );
                }            
        }

    }
}
