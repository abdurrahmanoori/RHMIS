using AutoMapper;
using PHMIS.Application.DTO.Patients;
using PHMIS.Domain.Entities;

namespace PHMIS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Patient, PatientCreateDto>().ReverseMap();
    }
}