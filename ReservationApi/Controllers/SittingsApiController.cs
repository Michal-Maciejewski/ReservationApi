using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Contracts.Interfaces;
using ReservationApi.Data;
using ReservationApi.Models.Sitting;

namespace ReservationApi.Controllers
{
    [Route("sitting")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SittingsApiController : BaseApiController
    {
        private readonly ISittingsService _sittingsService;
        public SittingsApiController(IMapper mapper, ILogger<BaseApiController> logger, ISittingsService sittingsService) : base(mapper, logger)
        {
            _sittingsService = sittingsService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSittings()
        {
            var test = new List<Sitting>{ new Sitting {Id = 1 }, new Sitting { Id = 1, GroupSittingId = 1 } };
            //var test = await _sittingsService.GetSittings();
            var model = test.Adapt<List<SittingBaseEventModel>>();
            return Ok(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createSittingModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createsitting")]
        public async Task<IActionResult> CreateSitting(CreateSittingModel createSittingModel)
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createSittingScheduleModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSittingSchedule(CreateSittingScheduleModel createSittingScheduleModel)
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sittingEventModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatesitting")]
        public async Task<IActionResult> UpdateSitting(SittingEventModel sittingEventModel)
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sittingGroupEventModels"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateSittingSchedule(List<SittingGroupEventModel> sittingGroupEventModels)
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteSitting(string id)
        {
            return Ok();
        }
    }
}
