using HCEHB.ScoreService.Web.Controllers.WebApiModels.Request;
using HCEHB.ScoreService.Web.Filters;
using HCEHB.ScoreService.Web.Models;
using Honeywell.HCEHB.DatabaseModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HCEHB.ScoreService.Web.Controllers
{
    using HCEHB.ScoreService.Service.DependentInterfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly IHealthyBuildingRepository _healthyBuildingRepository;

        public ScoresController(IHealthyBuildingRepository healthyBuildingRepository)
        {
            _healthyBuildingRepository = healthyBuildingRepository;
        }

        private string[] ParseFacilities()
        {
            if (!HttpContext.Request.Headers.ContainsKey("Facilities"))
                return new string[0];

            string facilityHeaderContent = HttpContext.Request.Headers["Facilities"];
            var facilities = facilityHeaderContent
                .Split(",")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(f => f.Trim()).ToArray();
            return facilities.Any() ? facilities.ToArray() : new string[0];
        }
        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        /// <summary>
        /// // GET api/Scores/
        /// </summary>
        /// <param name="request">TimeStamp: 20/06/2020 10:10:10</param>
        /// <returns>List RecentBuildingScoreAggregation</returns>
        [ProducesResponseType(200, Type = typeof(List<BuildingScore>))]
        [HttpGet]
        [LogActionTimingStatistics]
        public IActionResult Get([FromQuery] ScoreRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.TimeStamp))
                {
                    return BadRequest("Timestamp not set in the request");
                }
                string[] facilityIds = ParseFacilities();
                if (facilityIds.Length == 0)
                {
                    Log.Information($"GetAll returning BadRequest & facility parameter not set in request headers");
                    return BadRequest("facility parameter not set");
                }

                DateTime dateTimeFormatted = RoundUp(DateTime.Parse(request.TimeStamp), TimeSpan.FromMinutes(30));
                var buildingScores = _healthyBuildingRepository
                    .GetHealthyBuildingScoreTask(ParseFacilities(), dateTimeFormatted).Result;

                if (buildingScores.Length == 0)
                {
                    Log.Information($"returning 404 & no data found for Facility ids");
                    return NotFound("no data found for Facility ids");
                }

                return Ok(buildingScores);
            }
            catch (Exception ex)
            {
                Log.Error($"exception {ex}");
                return StatusCode(500, new { Status = "Error", Message = "Failed in fetching recent aggregated score for healthy buildings" });
            }
        }

        /// <summary>
        /// GET api/Scores/recent
        /// </summary>
        /// <returns>List RecentBuildingScore</returns>
        [ProducesResponseType(200, Type = typeof(List<RecentAvailableScore>))]
        [HttpGet("Recent")]
        [LogActionTimingStatistics]
        public IActionResult Recent()
        {
            try
            {
                string[] facilityIds = ParseFacilities();
                if (facilityIds.Length == 0)
                {
                    Log.Information($"GetAll returning BadRequest & facility parameter not set in request headers");
                    return BadRequest("facility parameter not set");
                }

                List<RecentAvailableScore> recentAvailableScores = new List<RecentAvailableScore>();
                foreach (var facility in facilityIds)
                {
                    RecentAvailableScore recentAvailableScore = new RecentAvailableScore { FacilityId = facility, TimeStamp = DateTime.Now };
                    recentAvailableScores.Add(recentAvailableScore);
                }

                return Ok(recentAvailableScores);
            }
            catch (Exception ex)
            {
                Log.Error($"exception {ex}");
                return StatusCode(500, new { Status = "Error", Message = "Failed in fetching recent timestamp for healthy building score" });
            }
        }

    }
}
