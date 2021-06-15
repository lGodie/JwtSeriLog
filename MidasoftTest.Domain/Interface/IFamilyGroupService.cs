using MidasoftTest.Common.Requests;
using MidasoftTest.Common.Responses;
using MidasoftTest.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Domain.Interface
{
    public interface IFamilyGroupService: ICrud<FamilyGroup>
    {
        Task<Response<IEnumerable<FamilyGroup>>> GetAllDapper();
        Task<Response<FamilyGroup>> GetByUserName(string userName);
    }
}
