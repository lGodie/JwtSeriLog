using MidasoftTest.Common.Responses;
using MidasoftTest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Domain.Interface
{
    public interface ICrud<T>
    {
        Task<Response<List<T>>> GetAll();
        Task<Response<T>> Create(T entity);
        Task<Response<T>>GetById(Guid id);
        Task<Response<bool>> Update(T entity);
        Task<Response<bool>> Delete(Guid id);
    }
}
