using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticlPro.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ArticlPro.Data.SqlServerEF
{
    public class CategoryEntity : IDataHelper<Category>
    {
        private  DBContext db;
        private Category _table;
        public CategoryEntity()
        {
            db = new DBContext();
            
            
        }
        public int Add(Category table)
        {
            if (db.Database.CanConnect())
            {
                db.Category.Add(table);
                return db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public int Delete(int Id)
        {
            if (db.Database.CanConnect())
            {
                _table= Find(Id);
                db.Category.Remove(_table);
                return db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public int Edit(int Id, Category table)
        {
            db = new DBContext();
            if (db.Database.CanConnect())
            {
                db.Category.Update(table);
                return db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public Category Find(int Id)
        {
           if(db.Database.CanConnect())
            {
                return db.Category.Where(x => x.Id == Id).First();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<Category> GetAllData()
        {
            if (db.Database.CanConnect())
            {
                return db.Category.ToList();
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
            if (db.Database.CanConnect())
            {
                return db.Category.Where(x=>x.Name.Contains(SearchItem)
                || x.Id.ToString().Contains(SearchItem))
                .ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }
    }
}
