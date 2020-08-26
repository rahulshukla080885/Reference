using System;

namespace HCEHB.ScoreService.Web.Models
{
    public class HealthData
    {
        public string FacilityId { get; set; }
        public string FacilityName { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Score { get; set; }
        public int ScoreCategory { get; set; }
        public int IciMetricScore { get; set; }
        public int IciMetricCategory { get; set; }
        public int PollutantMetricScore { get; set; }
        public int PollutantMetricCategory { get; set; }
    }
}
