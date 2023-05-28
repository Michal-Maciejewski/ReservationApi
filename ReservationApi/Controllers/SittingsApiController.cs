using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Contracts.Interfaces;
using ReservationApi.Data;
using ReservationApi.Models.Sitting;
using System.Text.Json.Serialization;

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
            var sittings = await _sittingsService.GetSittings();
            var model = sittings.Adapt<List<SittingBaseEventModel>>();
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetSittingsCriteria(GetEventModel getEventModel)
        {
            var sittings = await _sittingsService.GetSittingsRange(getEventModel.Start, getEventModel.End);
            return Ok(sittings);
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
            var sittingType = await _sittingsService.GetSittingTypeIdAsync(1);
            var sitting = createSittingModel.Adapt<Sitting>();
            sitting.SittingType = sittingType;
            sitting = await _sittingsService.CreateSitting(sitting);
            var model = sitting.Adapt<SittingBaseEventModel>();
            return Ok(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createSittingScheduleModel"></param>
        /// <returns></returns>
        [Route("createschedule")]
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
        public async Task<IActionResult> UpdateSitting(SittingBaseEventModel sittingEventModel)
        {
            var sitting = await _sittingsService.GetSitting(sittingEventModel.Id);
            if (sitting != null)
            {
                sitting.Start = sittingEventModel.Start;
                sitting.End = sittingEventModel.End;
                sitting.Notes = sittingEventModel.Notes;
                sitting.Title = sittingEventModel.Title;
                await _sittingsService.UpdateSitting(sitting);
                return Ok(sitting);
            }
            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateGroupSittingModel"></param>
        /// <returns></returns>
        [Route("updateschedule")]
        [HttpPut]
        public async Task<IActionResult> UpdateSittingSchedule(UpdateGroupSittingModel updateGroupSittingModel)
        {
            var sittings = await _sittingsService.GetSittings();
            sittings = sittings.Where(a => a.GroupSittingId == updateGroupSittingModel.GroupId).ToList();
            TimeSpan durationDiffEnd = updateGroupSittingModel.EndNew - updateGroupSittingModel.End;
            TimeSpan durationDiffStart = updateGroupSittingModel.StartNew - updateGroupSittingModel.Start;
            foreach(var situation in sittings)
            {
                situation.End += durationDiffEnd;
                situation.Start += durationDiffStart;
            }
            await _sittingsService.UpdateSittingGroup(sittings);

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
