using MediatR;
using Microsoft.AspNetCore.Mvc;
using PHMIS.Application.Common;
using PHMIS.Application.DTO.Patients;
using PHMIS.Application.Features.Patients.Commands;
using PHMIS.Application.Features.Patients.Queries;
using PHMIS.Controllers.Base;

namespace PHMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<PatientCreateDto>> Create(PatientCreateDto dto) =>
         HandleResultResponse(await Mediator.Send(new CreatePatientCommand(dto)));

        [HttpGet]
        public async Task<ActionResult<PagedList<PatientDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25) =>
            HandleResultResponse(await Mediator.Send(new GetPatientListQuery(pageNumber, pageSize)));

        
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id) =>
            HandleResultResponse(await Mediator.Send(new GetPatientByIdQuery(id)));

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> Update(int id, PatientCreateDto dto) =>
            HandleResultResponse(await Mediator.Send(new UpdatePatientCommand(id, dto)));

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id) =>
            HandleResultResponse(await Mediator.Send(new DeletePatientCommand(id)));
    }
}
