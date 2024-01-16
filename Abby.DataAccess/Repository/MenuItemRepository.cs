using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;

namespace Abby.DataAccess.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDBContext _db;

        public MenuItemRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
       
        public void Update(MenuItem menuItem)
        { 
            var objFromDb = _db.MenuItem.FirstOrDefault( u => u.Id == menuItem.Id );
            if( objFromDb != null)
            {
                objFromDb.Name = menuItem.Name;
                objFromDb.Description = menuItem.Description;
                objFromDb.Price = menuItem.Price;
                objFromDb.CategoryId = menuItem.CategoryId;
                objFromDb.Category = menuItem.Category;
                objFromDb.FoodType = menuItem.FoodType;
                objFromDb.FoodTypeId = menuItem.FoodTypeId;
                if(menuItem.Image != null)
                {
                    objFromDb.Image = menuItem.Image;
                }
            }
        }
    }
}
