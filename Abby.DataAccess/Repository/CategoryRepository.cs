﻿using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _db;

        public CategoryRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
       

        public void Update(Category category)
        { 
            var objFromDb = _db.Category.FirstOrDefault( u => u.Id == category.Id );
            if( objFromDb != null)
            {
                objFromDb.Name = category.Name;
                objFromDb.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}
