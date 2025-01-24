using BuddysKitchen.Core;
using BuddysKitchen.Data;
using BuddysKitchen.Entities;
using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuddysKitchen.Services
{
    public class DirectionService : IDirectionService
    {
        private IDataContext DataContext { get; }

        public DirectionService(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<List<DirectionModel>> GetAllAsync()
        {
            var entities = await DataContext.Directions.ToListAsync();
            return entities.Select(d => d.Copy<Direction, DirectionModel>()).ToList();
        }

        public async Task<DirectionModel?> GetAsync(long id)
        {
            var entity = await DataContext.Directions.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (entity == null)
                return null;

            return entity.Copy<Direction, DirectionModel>();
        }

        public async Task<DirectionModel> AddAsync(DirectionModel model)
        {
            var entity = model.Copy<DirectionModel, Direction>();
            DataContext.Directions.Add(entity);
            await DataContext.SaveChangesAsync();

            return entity.Copy<Direction, DirectionModel>();
        }

        public async Task<DirectionModel?> UpdateAsync(DirectionModel model)
        {
            var entity = DataContext.Directions.Where(d => d.Id == model.Id).FirstOrDefault();
            if (entity == null)
                return null;

            // Update DB
            Map(entity, model);
            await DataContext.SaveChangesAsync();

            return entity.Copy<Direction, DirectionModel>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = DataContext.Directions.FirstOrDefault(r => r.Id == id);
            if (entity == null)
                return false;

            DataContext.Directions.Remove(entity);
            await DataContext.SaveChangesAsync();
            return true;
        }

        private static void Map(Direction dbItem, DirectionModel model)
        {
            dbItem.Id = model.Id;
            dbItem.StepNumber = model.StepNumber;
            dbItem.Description = model.Description;
        }
    }
}
