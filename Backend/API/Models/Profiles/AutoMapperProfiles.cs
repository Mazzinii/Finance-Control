using AutoMapper;
using PersonTransation.Models.DTOs;
using PersonTransation.Models.Entities;

namespace PersonTransation.Models.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //User
            CreateMap<UserModel, UserDTO>();

            //Transation 
            CreateMap<TransactionModel, TransactionDTO>();
            
        }
    }
}
