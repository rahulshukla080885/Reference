// ************************************************************************
// *****      COPYRIGHT 2014 - 2018 HONEYWELL INTERNATIONAL SARL      *****
// ************************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCEHB.ScoreService.TodoService.DependentInterfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetItem(Guid id);

        Task<IEnumerable<TodoItem>> GetAllItems();

        Task<bool> AddItem(TodoItem item);

        Task<bool> RemoveItem(Guid id);

        bool RemoveItem(Guid id, out TodoItem removedItem);
    }
}