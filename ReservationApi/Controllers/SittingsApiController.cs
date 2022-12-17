using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Contracts.Interfaces;
using ReservationApi.Data;
using ReservationApi.Models.Sitting;

namespace ReservationApi.Controllers
{
    [Route("sitting")]
    [ApiController]
    public class SittingsApiController : ControllerBase
    {
        private readonly ISittingsService _sittingsService;
        public SittingsApiController(ISittingsService sittingsService)
        {
            _sittingsService = sittingsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSittings()
        {
            var test = new List<Sitting>{ new Sitting {Id = 1 }, new Sitting { Id = 1, GroupSittingId = 1 } };
            //var test = await _sittingsService.GetSittings();
            var newTest = test.Adapt<List<SittingBaseEventModel>>();
            return Ok();
        }

        [HttpPost]
        [Route("createsitting")]
        public async Task<IActionResult> CreateSitting(CreateSittingModel createSittingModel)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSittingSchedule(CreateSittingScheduleModel createSittingScheduleModel)
        {
            return Ok();
        }

        [HttpPut]
        [Route("updatesitting")]
        public async Task<IActionResult> UpdateSitting(SittingEventModel sittingEventModel)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSittingSchedule(List<SittingGroupEventModel> sittingGroupEventModels)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSitting(string id)
        {
            return Ok();
        }
    }
}
