using MediatR;
using Microsoft.AspNetCore.Mvc;
using PHMIS.Application.Common;
using PHMIS.Application.DTO.Provinces;
using PHMIS.Application.Features.Provinces.Commands;
using PHMIS.Application.Features.Provinces.Queries;
using PHMIS.Controllers.Base;

namespace PHMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : BaseApiController
    {
        private readonly IMediator _mediator;
        public ProvinceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ProvinceCreateDto>> Create(ProvinceCreateDto dto) =>
            HandleResultResponse(await _mediator.Send(new CreateProvinceCommand(dto)));

        [HttpGet]
        public async Task<ActionResult<PagedList<ProvinceDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25) =>
            HandleResultResponse(await _mediator.Send(new GetProvinceListQuery(pageNumber, pageSize)));

        [HttpGet("{id}")]
        public async Task<ActionResult<ProvinceDto>> GetById(int id) =>
            HandleResultResponse(await _mediator.Send(new GetProvinceByIdQuery(id)));

        [HttpPut("{id}")]
        public async Task<ActionResult<ProvinceDto>> Update(int id, ProvinceCreateDto dto) =>
            HandleResultResponse(await _mediator.Send(new UpdateProvinceCommand(id, dto)));

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id) =>
            HandleResultResponse(await _mediator.Send(new DeleteProvinceCommand(id)));
    }
}
