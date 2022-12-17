using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReservationApi.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public IMapper _mapper { get; set; }
        public ILogger _logger { get; set; }
        public BaseApiController(IMapper mapper, ILogger<BaseApiController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
    }
}
