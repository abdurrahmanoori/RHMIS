using MediatR;
using Microsoft.AspNetCore.Mvc;
using PHMIS.Application.Common;
using PHMIS.Application.DTO.Laboratory;
using PHMIS.Application.Features.Laboratory.LabTests.Commands;
using PHMIS.Application.Features.Laboratory.LabTests.Queries;
using PHMIS.Controllers.Base;

namespace PHMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : BaseApiController
    {
        private readonly IMediator _mediator;
        public LabTestController(IMediator mediator) { _mediator = mediator; }

        [HttpPost]
        public async Task<ActionResult<LabTestCreateDto>> Create(LabTestCreateDto dto) =>
            HandleResultResponse(await _mediator.Send(new CreateLabTestCommand(dto)));

        [HttpGet]
        public async Task<ActionResult<PagedList<LabTestDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25) =>
            HandleResultResponse(await _mediator.Send(new GetLabTestListQuery(pageNumber, pageSize)));

        [HttpGet("{id}")]
        public async Task<ActionResult<LabTestDto>> GetById(int id) =>
            HandleResultResponse(await _mediator.Send(new GetLabTestByIdQuery(id)));

        [HttpPut("{id}")]
        public async Task<ActionResult<LabTestDto>> Update(int id, LabTestCreateDto dto) =>
            HandleResultResponse(await _mediator.Send(new UpdateLabTestCommand(id, dto)));

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id) =>
            HandleResultResponse(await _mediator.Send(new DeleteLabTestCommand(id)));
    }
}
