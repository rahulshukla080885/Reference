// ************************************************************************
// *****      COPYRIGHT 2014 - 2018 HONEYWELL INTERNATIONAL SARL      *****
// ************************************************************************

using System;
using System.Threading.Tasks;

namespace HCEHB.ScoreService.TodoService
{
    

    public interface ITodoService
    {
        Task<GetResults> GetAll();

        Task<AddResult> Add(string title, string description);

        Task<UpdateResult> Update(Guid id, string title, string description, bool? isFlagged);

        Task<DeleteResult> Delete(Guid id);

        Task<GetResult> GetById(Guid id);
    }
}