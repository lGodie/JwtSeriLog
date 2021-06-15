using MidasoftTest.Common.Requests;
using MidasoftTest.Common.Responses;
using MidasoftTest.Data.DataContext;
using MidasoftTest.Data.Repositories.Interface;
using MidasoftTest.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Domain.Services
{
    public class FamilyGroupService : IFamilyGroupService
    {
        private readonly IFamilyGroupRepository _familyGroupRepositories;
        private readonly IGenericRepository _genericRepository;

        public  FamilyGroupService(IFamilyGroupRepository familyGroupRepositories, IGenericRepository genericRepository)
        {
            _familyGroupRepositories = familyGroupRepositories;
            _genericRepository = genericRepository;
        }
        public async Task<Response<IEnumerable<FamilyGroup>>> GetAllDapper()
        {
            IEnumerable<FamilyGroup> list = await _familyGroupRepositories.GetAllDapperAsync();
           
            return new Response<IEnumerable<FamilyGroup>>
            {
                IsSuccess = true,
                Result = list
            };
        }
        public async Task<Response<FamilyGroup>> GetByUserName(string userName)
        {
            FamilyGroup list = await _familyGroupRepositories.GetByUserDapperName(userName);

            return new Response<FamilyGroup>
            {
                IsSuccess = true,
                Result = list
            };
        }
        public async Task<Response<bool>> Delete(Guid RequestId)
        {
            FamilyGroup exits = await _genericRepository.GetByIdAsync<FamilyGroup>(t => t.Id == RequestId);
            if (exits == null)
            {
                return new Response<bool>
                {
                    IsSuccess = false
                };
            }
            return new Response<bool>
            {
                IsSuccess = true
            };
        }

        public async Task<Response<List<FamilyGroup>>> GetAll()
        {
           List<FamilyGroup> list = await _genericRepository.GetAllAsync<FamilyGroup>();

            return new Response<List<FamilyGroup>>
            { 
                  IsSuccess=true,
                  Result= list
            };
        }

        public async Task<Response<FamilyGroup>> GetById(Guid RequestId)
        {
            FamilyGroup exits = await _genericRepository.GetByIdAsync<FamilyGroup>(t => t.Id == RequestId);
            if (exits == null)
            {
                return new Response<FamilyGroup>
                {
                    IsSuccess = false
                };
            }
            return new Response<FamilyGroup>
            {
             IsSuccess=true,
             Result= exits
            };
        }
        public async Task<Response<bool>> Update(FamilyGroup entity)
        {
             _genericRepository.Update<FamilyGroup>(entity);
            return new Response<bool>{
                 IsSuccess=true
            };
        }

        public async  Task<Response<FamilyGroup>> Create(FamilyGroup entity)
        {
           FamilyGroup exits = await _genericRepository.GetByIdAsync<FamilyGroup>(t=>t.UserName == entity.UserName);

            if (exits!= null)
            {
                return new Response<FamilyGroup>{
                       IsSuccess=false,
                       Message="Ya se encuentra registrado"
                };
            }

            if (entity.Age < 18)
            {
                if (entity.BirthDate == null)
                {
                    return new Response<FamilyGroup>
                    {
                        IsSuccess = false,
                        Message = "La fecha de nacimiento es requerida"
                    };
                }
                entity.younger = true;
            }


            FamilyGroup save = await _genericRepository.CreateAsync<FamilyGroup>(entity);

            return new Response<FamilyGroup>
            {
                IsSuccess = true,
                Message = "Exitoso"
            };
        }
       
    }
}
