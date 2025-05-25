using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticlPro.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ArticlPro.Data.SqlServerEF
{
    public class AuthorPostEntity : IDataHelper<AuthorPost>
    {
        private  DBContext db;
        private AuthorPost _table;
        public AuthorPostEntity()
        {
            db = new DBContext();
            
            
        }
        public int Add(AuthorPost table)
        {
            if (db.Database.CanConnect())
            {
                db.AuthorPost.Add(table);
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
                db.AuthorPost.Remove(_table);
                return db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public int Edit(int Id, AuthorPost table)
        {
            db = new DBContext();
            if (db.Database.CanConnect())
            {
                db.AuthorPost.Update(table);
                return db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public AuthorPost Find(int Id)
        {
           if(db.Database.CanConnect())
            {
                return db.AuthorPost.Where(x => x.Id == Id).First();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<AuthorPost> GetAllData()
        {
            if (db.Database.CanConnect())
            {
                return db.AuthorPost.ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<AuthorPost> GetDataByUser(string UserId)
        {
            if (db.Database.CanConnect())
            {
                return db.AuthorPost.Where(x=>x.UserId==UserId).ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<AuthorPost> Search(string SearchItem)
        {
            if (db.Database.CanConnect())
            {
                return db.AuthorPost.Where(x =>
               x.FullName.Contains(SearchItem)
               || x.UserId.Contains(SearchItem)
               || x.UserName.Contains(SearchItem)
               || x.PostTitle.Contains(SearchItem)
               || x.PostDescription.Contains(SearchItem)
               || x.PostImageUrl.Contains(SearchItem)
               || x.AuthorId.ToString().Contains(SearchItem)
               || x.CategoryId.ToString().Contains(SearchItem)
               || x.AddedDate.ToString().Contains(SearchItem)
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
