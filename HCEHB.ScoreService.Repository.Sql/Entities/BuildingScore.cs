
namespace HCEHB.ScoreService.Repository.Sql.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BuildingScore")]
    public class BuildingScore
    {
        public string FacilityId { get; set; }

        public string SiteId { get; set; }

        public string SiteName { get; set; }

        public string ZoneId { get; set; }

        public string ZoneName { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public decimal Score { get; set; }

        public int ScoreCategory { get; set; }

        public decimal IndoorClimateIndex { get; set; }

        public int IndoorClimateIndexCategory { get; set; }

        public decimal PollutantIndex { get; set; }

        public int PollutantIndexCategory { get; set; }
    }
}
