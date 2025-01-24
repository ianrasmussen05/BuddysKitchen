using BuddysKitchen.Core;
using BuddysKitchen.Data;
using BuddysKitchen.Entities;
using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuddysKitchen.Services
{
    public class CuisineService : ICuisineService
    {
        private IDataContext DataContext { get; }

        public CuisineService(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<List<CuisineModel>> GetAllAsync()
        {
            var entities = await DataContext.Cuisines.ToListAsync();
            return entities.Select(c => c.Copy<Cuisine, CuisineModel>()).ToList();
        }

        public async Task<CuisineModel?> GetAsync(long id)
        {
            var entity = await DataContext.Cuisines.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (entity == null)
                return null;

            return entity.Copy<Cuisine, CuisineModel>();
        }

        public async Task<CuisineModel> AddAsync(CuisineModel model)
        {
            var entity = model.Copy<CuisineModel, Cuisine>();
            DataContext.Cuisines.Add(entity);
            await DataContext.SaveChangesAsync();

            return entity.Copy<Cuisine, CuisineModel>();
        }

        public async Task<CuisineModel?> UpdateAsync(CuisineModel model)
        {
            var entity = DataContext.Cuisines.Where(c => c.Id == model.Id).FirstOrDefault();
            if (entity == null)
                return null;

            // Update DB
            Map(entity, model);
            await DataContext.SaveChangesAsync();

            return entity.Copy<Cuisine, CuisineModel>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = DataContext.Cuisines.FirstOrDefault(c => c.Id == id);
            if (entity == null)
                return false;

            DataContext.Cuisines.Remove(entity);
            await DataContext.SaveChangesAsync();
            return true;
        }

        private static void Map(Cuisine dbItem, CuisineModel model)
        {
            dbItem.Id = model.Id;
            dbItem.Name = model.Name;
        }
    }
}
