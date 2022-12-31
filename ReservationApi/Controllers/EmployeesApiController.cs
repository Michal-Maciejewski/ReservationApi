using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EmployeesApiController : BaseApiController
    {
        public EmployeesApiController(IMapper mapper, ILogger<BaseApiController> logger) : base(mapper, logger)
        {

        }
    }
}
