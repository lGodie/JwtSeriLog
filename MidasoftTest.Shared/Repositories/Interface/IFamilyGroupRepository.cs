using MidasoftTest.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Data.Repositories.Interface
{
    public interface IFamilyGroupRepository
    {
        Task<IEnumerable<FamilyGroup>> GetAllDapperAsync();
        Task<FamilyGroup> GetByUserDapperName(string userName);
    }
}
