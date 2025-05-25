using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticlPro.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ArticlPro.Data.SqlServerEF
{
    public class AuthorEntity : IDataHelper<Author>
    {
        private  DBContext db;
        private Author _table;
        public AuthorEntity()
        {
            db = new DBContext();
            
            
        }
        public int Add(Author table)
        {
            if (db.Database.CanConnect())
            {
                db.Author.Add(table);
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
                db.Author.Remove(_table);
                return db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public int Edit(int Id, Author table)
        {
            db = new DBContext();
            if (db.Database.CanConnect())
            {
                db.Author.Update(table);
                return db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public Author Find(int Id)
        {
           if(db.Database.CanConnect())
            {
                return db.Author.Where(x => x.Id == Id).First();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<Author> GetAllData()
        {
            if (db.Database.CanConnect())
            {
                return db.Author.ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<Author> GetDataByUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<Author> Search(string SearchItem)
        {
            if (db.Database.CanConnect())
            {
                return db.Author.Where(
                 x=>x.FullName.Contains(SearchItem)
                || x.UserId.Contains(SearchItem)
                || x.Bio.ToString().Contains(SearchItem)
                || x.UserName.ToString().Contains(SearchItem)
                || x.Facbook.ToString().Contains(SearchItem)
                || x.Twitter.ToString().Contains(SearchItem)
                || x.Instagram.ToString().Contains(SearchItem)
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
