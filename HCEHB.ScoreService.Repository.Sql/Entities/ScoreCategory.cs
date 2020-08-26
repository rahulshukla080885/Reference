namespace HCEHB.ScoreService.Repository.Sql.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ScoreCategory")]
    public class ScoreCategory
    {
        public int ScoreCategoryId { get; set; }

        public int MinimumScore { get; set; }

        public int MaximumScore { get; set; }
    }
}
