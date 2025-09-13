using MediatR;
using Microsoft.AspNetCore.Mvc;
using PHMIS.Application.Common;
using PHMIS.Application.DTO.Hospitals;
using PHMIS.Application.Features.Hospitals.Commands;
using PHMIS.Application.Features.Hospitals.Queries;
using PHMIS.Controllers.Base;

namespace PHMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<HospitalDto>> Create(HospitalCreateDto dto) =>
            HandleResultResponse(await Mediator.Send(new CreateHospitalCommand(dto)));

        [HttpGet]
        public async Task<ActionResult<PagedList<HospitalDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25) =>
            HandleResultResponse(await Mediator.Send(new GetHospitalListQuery(pageNumber, pageSize)));

        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalDto>> GetById(int id) =>
            HandleResultResponse(await Mediator.Send(new GetHospitalByIdQuery(id)));

        [HttpPut("{id}")]
        public async Task<ActionResult<HospitalDto>> Update(int id, HospitalCreateDto dto) =>
            HandleResultResponse(await Mediator.Send(new UpdateHospitalCommand(id, dto)));

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id) =>
            HandleResultResponse(await Mediator.Send(new DeleteHospitalCommand(id)));
    }
}
