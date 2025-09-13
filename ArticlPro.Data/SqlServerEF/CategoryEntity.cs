using System;
using System.Collections.Generic;
using System.Linq;
using ArticlPro.Core;

namespace ArticlPro.Data.SqlServerEF
{
    public class CategoryEntity : IDataHelper<Category>
    {
        private readonly DBContext _db;

        // الكونستركتور الآن يأخذ DBContext من DI
        public CategoryEntity(DBContext dbContext)
        {
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public int Add(Category table)
        {
            if (_db.Database.CanConnect())
            {
                _db.Category.Add(table);
                return _db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public int Delete(int Id)
        {
            if (_db.Database.CanConnect())
            {
                var _table = Find(Id);
                _db.Category.Remove(_table);
                return _db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public int Edit(int Id, Category table)
        {
            if (_db.Database.CanConnect())
            {
                _db.Category.Update(table);
                return _db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public Category Find(int Id)
        {
            if (_db.Database.CanConnect())
            {
                return _db.Category.FirstOrDefault(x => x.Id == Id)
                       ?? throw new Exception("Record not found");
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<Category> GetAllData()
        {
            if (_db.Database.CanConnect())
            {
                return _db.Category.ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<Category> GetDataByUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<Category> Search(string SearchItem)
        {
            if (_db.Database.CanConnect())
            {
                return _db.Category.Where(x =>
                    x.Name.Contains(SearchItem) ||
                    x.Id.ToString().Contains(SearchItem)
                ).ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }
    }
}
