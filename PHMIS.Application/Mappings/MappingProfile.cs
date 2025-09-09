using AutoMapper;
using PHMIS.Application.DTO.Patients;
using PHMIS.Application.DTO.Provinces;
using PHMIS.Domain.Entities;

namespace PHMIS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Patient, PatientCreateDto>().ReverseMap();

        CreateMap<Province, ProvinceDto>().ReverseMap();
        CreateMap<Province, ProvinceCreateDto>().ReverseMap();
    }
}