using Auditory.API.Models;
using AutoMapper;

namespace Auditory.API.Data.Mappers
{
    public class UserRecordHistoryProfile : Profile
    {
        public UserRecordHistoryProfile()
        {
            CreateMap<UserRecordHistory, UserRecordHistoryMongo>().ReverseMap();
        }
    }
}
