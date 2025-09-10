using AutoMapper;
using PHMIS.Application.DTO.Patients;
using PHMIS.Application.DTO.Provinces;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Domain.Entities;
using PHMIS.Domain.Entities.Laboratory;
using PHMIS.Domain.Entities.Patients;

namespace PHMIS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Patient, PatientCreateDto>().ReverseMap();

        CreateMap<Province, ProvinceDto>().ReverseMap();
        CreateMap<Province, ProvinceCreateDto>().ReverseMap();

        CreateMap<LabTestGroup, LabTestGroupDto>().ReverseMap();
        CreateMap<LabTestGroup, LabTestGroupCreateDto>().ReverseMap();

        CreateMap<LabTest, LabTestDto>().ReverseMap();
        CreateMap<LabTest, LabTestCreateDto>().ReverseMap();
    }
}