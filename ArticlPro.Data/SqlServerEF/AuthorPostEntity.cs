using System;
using System.Collections.Generic;
using System.Linq;
using ArticlPro.Core;

namespace ArticlPro.Data.SqlServerEF
{
    public class AuthorPostEntity : IDataHelper<AuthorPost>
    {
        private readonly DBContext _db;

        // تعديل الكونستركتور ليأخذ DBContext من DI
        public AuthorPostEntity(DBContext dbContext)
        {
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public int Add(AuthorPost table)
        {
            if (_db.Database.CanConnect())
            {
                _db.AuthorPost.Add(table);
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
                _db.AuthorPost.Remove(_table);
                return _db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public int Edit(int Id, AuthorPost table)
        {
            if (_db.Database.CanConnect())
            {
                _db.AuthorPost.Update(table);
                return _db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public AuthorPost Find(int Id)
        {
            if (_db.Database.CanConnect())
            {
                return _db.AuthorPost.FirstOrDefault(x => x.Id == Id)
                    ?? throw new Exception("Record not found");
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<AuthorPost> GetAllData()
        {
            if (_db.Database.CanConnect())
            {
                return _db.AuthorPost.ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<AuthorPost> GetDataByUser(string UserId)
        {
            if (_db.Database.CanConnect())
            {
                return _db.AuthorPost.Where(x => x.UserId == UserId).ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<AuthorPost> Search(string SearchItem)
        {
            if (_db.Database.CanConnect())
            {
                return _db.AuthorPost.Where(x =>
                    x.FullName.Contains(SearchItem) ||
                    x.UserId.Contains(SearchItem) ||
                    x.UserName.Contains(SearchItem) ||
                    x.PostTitle.Contains(SearchItem) ||
                    x.PostDescription.Contains(SearchItem) ||
                    x.PostImageUrl.Contains(SearchItem) ||
                    x.AuthorId.ToString().Contains(SearchItem) ||
                    x.CategoryId.ToString().Contains(SearchItem) ||
                    x.AddedDate.ToString().Contains(SearchItem) ||
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
