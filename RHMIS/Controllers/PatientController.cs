using MediatR;
using Microsoft.AspNetCore.Mvc;
using PHMIS.Application.DTO.Patients;
using PHMIS.Application.Features.Patients.Commands;
using PHMIS.Controllers.Base;

namespace PHMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : BaseApiController
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<PatientCreateDto>> Create(PatientCreateDto dto) =>
         HandleResultResponse(await _mediator.Send(new CreatePatientCommand(dto)));

        //[HttpGet]
        //public async Task<ActionResult<List<CustomerResponseDto>>> GetAll() =>
        //    HandleResultResponse(await _customerService.GetCustomerList());

        //[HttpGet("{id}")]
        //public async Task<ActionResult<CustomerResponseDto>> GetById(int id) =>
        //    HandleResultResponse(await _customerService.GetCustomerById(id));

        //[HttpPut("{id}")]
        //public async Task<ActionResult<bool>> Update(int id, CustomerDto dto) =>
        //    HandleResultResponse(await _customerService.UpdateCustomer(id, dto));

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> Delete(int id) =>
        //    HandleResultResponse(await _customerService.DeleteCustomer(id));
    }
}
