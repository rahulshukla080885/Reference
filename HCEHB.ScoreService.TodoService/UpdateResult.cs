// ************************************************************************
// *****      COPYRIGHT 2014 - 2018 HONEYWELL INTERNATIONAL SARL      *****
// ************************************************************************

namespace HCEHB.ScoreService.TodoService
{
    public class UpdateResult
    {
        public bool IsNotFoundError { get; set; }

        public bool IsSuccess { get; set; }

        public TodoItem UpdatedItem { get; set; }
    }
}