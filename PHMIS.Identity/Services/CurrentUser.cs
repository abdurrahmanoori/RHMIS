using PHMIS.Application.Identity.IServices;


namespace PHMIS.Identity.Services
{
    public  class CurrentUser: ICurrentUser
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(/*IHttpContextAccessor httpContextAccessor*/)
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        public int? GetUserId()
        {
            //var user = _httpContextAccessor.HttpContext?.User;
            //if (user?.Identity?.IsAuthenticated ?? false)
            //{
            //    // Extract user ID from claims
            //    var IrdLoginID = user.FindFirst("IRD_LOGIN_ID")?.Value;
            //    return Task.FromResult<string>(IrdLoginID);
            //}

            //return Task.FromResult("NRZ01015");
            return 1;


            //int? currentUserId = Convert.ToInt32(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            //if (currentUserId is not null)
            //{
            //    return Task.FromResult(currentUserId);
            //}
            //return null!; // Or handle appropriately when user ID is missing
        }
    }
}
