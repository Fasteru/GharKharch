using AutoMapper;
using GharKharchaAPI.Data.Entities;
using GharKharchaAPI.Domain.Models;
using System.Runtime;

namespace GharKharchaAPI.Helper
{
    public class MapperHelper : Profile
    {
        private readonly IMapper _mapper;
        public MapperHelper()
        {
            CreateMap<GharKharchaAPI.Domain.Models.User, GharKharchaAPI.Data.Entities.UserEntity>();
            CreateMap<GharKharchaAPI.Domain.Models.Family, GharKharchaAPI.Data.Entities.FamilyEntity>();
            CreateMap<GharKharchaAPI.Domain.Models.Expense, GharKharchaAPI.Data.Entities.ExpenseEntity>();
            CreateMap<GharKharchaAPI.Data.Entities.UserEntity, GharKharchaAPI.Domain.Models.User>();
            CreateMap<GharKharchaAPI.Domain.Models.Family, GharKharchaAPI.Data.Entities.FamilyEntity>();
        }
       
    }
}
