// ************************************************************************
// *****      COPYRIGHT 2014 - 2018 HONEYWELL INTERNATIONAL SARL      *****
// ************************************************************************

namespace HCEHB.ScoreService.TodoService
{
    using System.Collections.Generic;

    public class GetResults
    {
        public bool IsSuccess { get; set; }

        public IEnumerable<TodoItem> Items { get; set; }
    }
}