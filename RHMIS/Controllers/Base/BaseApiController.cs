using Microsoft.AspNetCore.Mvc;
using PHMIS.Application.Common.Response;

namespace PHMIS.Controllers.Base
{
    //[Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public IActionResult HandleResult<T>(Result<T> response)
        {
            try
            {
                if (response.Success && response.Response == null)
                {
                    return NotFound();
                }
                else if (response.Success && response.Response != null)
                {
                    return Ok(response.Response);
                }
                else if (!response.Success && response.Errors != null)
                {
                    return BadRequest(response.Errors);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public ActionResult<T> HandleResultResponse<T>(Result<T> response)
        {
            try
            {
                if (response.Success && response.Response == null)
                {
                    return NotFound(response.Errors);
                }
                else if (!response.Success && response.Errors != null && response.Errors.Any(x => x.Code == DeclareMessage.Duplicate.Code))
                {
                    return Conflict(response.Errors);
                }
                else if (response.Success && response.Response != null)
                {
                    return Ok(response.Response);
                }
                else if (!response.Success && response.Errors != null)
                {
                    return BadRequest(response.Errors);
                }

                else
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
