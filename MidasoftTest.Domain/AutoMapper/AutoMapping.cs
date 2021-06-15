using AutoMapper;
using MidasoftTest.Common.Requests;
using MidasoftTest.Common.Responses;
using MidasoftTest.Data.DataContext;

namespace MidasoftTest.Domain.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<FamilyGroup, FamilyGroupRequest>();
            CreateMap<FamilyGroupRequest, FamilyGroup>();

            CreateMap<FamilyGroup, FamilyGroupResponse>();
            CreateMap<Users, UserRequest>();
            CreateMap<UserRequest, Users>();

            CreateMap<Users, UsersResponse>();
        }
    }
}
