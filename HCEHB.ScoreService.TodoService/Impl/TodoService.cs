﻿// ************************************************************************
// *****      COPYRIGHT 2014 - 2018 HONEYWELL INTERNATIONAL SARL      *****
// ************************************************************************

using System;
using System.Threading.Tasks;
using HCEHB.ScoreService.TodoService.DependentInterfaces;

namespace HCEHB.ScoreService.TodoService.Impl
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<AddResult> Add(string title, string description)
        {
            //TODO: Add security input validation for title and description strings
            try
            {
                var item = new TodoItem
                {
                    CreatedTimeStamp = DateTime.UtcNow,
                    Description = description,
                    IsCompleted = false,
                    Title = title,
                    Id = Guid.NewGuid()
                };

                var success = await _todoRepository.AddItem(item);
                return new AddResult {IsSuccess = success, NewItem = success ? item : null};
            }
            catch (Exception e)
            {
                Logger.LogException(e, $"Exception: unable to add new item with title: {title} description: {description}");
                return new AddResult {IsSuccess = false};
            }
        }

        public async Task<UpdateResult> Update(Guid id, string title, string description, bool? isFlagged)
        {
            try
            {
               if (id == Guid.Empty)
                   return new UpdateResult { UpdatedItem = null, IsSuccess = false, IsNotFoundError = true };

                if (_todoRepository.RemoveItem(id, out var item))
                {
                    if (title != null)
                        item.Title = title;
                    if (description != null)
                        item.Description = description;
                    if (isFlagged.HasValue)
                        item.IsCompleted = isFlagged.Value;

                    await _todoRepository.AddItem(item);
                    return new UpdateResult { UpdatedItem = item, IsSuccess = true, IsNotFoundError = false };
                }
                else
                {
                    return new UpdateResult { UpdatedItem = null, IsSuccess = false, IsNotFoundError = true };
                }
            }
            catch (Exception e)
            {
                Logger.LogException(e, $"Exception: unable to add update item with id: {id} title: {title} description: {description} isFlagged: {isFlagged}");
                return new UpdateResult {UpdatedItem = null, IsSuccess = false, IsNotFoundError = false};
            }
        }

        public async Task<GetResults> GetAll()
        {
            var failedResult = new GetResults {IsSuccess = false, Items = new TodoItem[0]};
            try
            {
                var items = await _todoRepository.GetAllItems();
                return items == null ? failedResult : new GetResults {IsSuccess = true, Items = items};
            }
            catch (Exception e)
            {
                Logger.LogException(e, "Exception: unable to get all items.");
                return failedResult;
            }
        }

        public async Task<GetResult> GetById(Guid id)
        {
            var failResult = new GetResult {IsSuccess = false};
            if (id == Guid.Empty)
                return failResult;

            try
            {
                var item = await _todoRepository.GetItem(id);
                return new GetResult {IsSuccess = item != null, Item = item};
            }
            catch (Exception e)
            {
                Logger.LogException(e, $"Exception: unable to get item with id: {id}");
                return failResult;
            }
        }

        public async Task<DeleteResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException();

                return await _todoRepository.RemoveItem(id)
                    ? new DeleteResult {IsSuccess = true, IsNotFoundError = false}
                    : new DeleteResult {IsSuccess = false, IsNotFoundError = true};
            }
            catch (Exception e)
            {
                Logger.LogException(e, $"Exception: unable to delete item with id: {id}");
                return new DeleteResult {IsSuccess = false, IsNotFoundError = false};
            }
        }
    }
}