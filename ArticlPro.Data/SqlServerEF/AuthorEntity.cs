using System;
using System.Collections.Generic;
using System.Linq;
using ArticlPro.Core;
using Microsoft.EntityFrameworkCore;

namespace ArticlPro.Data.SqlServerEF
{
    public class AuthorEntity : IDataHelper<Author>
    {
        private readonly DBContext _db;

        // استخدام DI لحقن DBContext
        public AuthorEntity(DBContext dbContext)
        {
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public int Add(Author table)
        {
            if (_db.Database.CanConnect())
            {
                _db.Author.Add(table);
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
                _db.Author.Remove(_table);
                return _db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public int Edit(int Id, Author table)
        {
            if (_db.Database.CanConnect())
            {
                _db.Author.Update(table);
                return _db.SaveChanges();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public Author Find(int Id)
        {
            if (_db.Database.CanConnect())
            {
                return _db.Author.FirstOrDefault(x => x.Id == Id)
                       ?? throw new Exception("Author not found");
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<Author> GetAllData()
        {
            if (_db.Database.CanConnect())
            {
                return _db.Author.ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<Author> GetDataByUser(string UserId)
        {
            if (_db.Database.CanConnect())
            {
                return _db.Author.Where(x => x.UserId == UserId).ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }

        public List<Author> Search(string SearchItem)
        {
            if (_db.Database.CanConnect())
            {
                return _db.Author
                    .Where(x =>
                        (x.FullName != null && x.FullName.Contains(SearchItem)) ||
                        (x.UserId != null && x.UserId.Contains(SearchItem)) ||
                        (x.Bio != null && x.Bio.Contains(SearchItem)) ||
                        (x.UserName != null && x.UserName.Contains(SearchItem)) ||
                        (x.Facbook != null && x.Facbook.Contains(SearchItem)) ||
                        (x.Twitter != null && x.Twitter.Contains(SearchItem)) ||
                        (x.Instagram != null && x.Instagram.Contains(SearchItem)) ||
                        x.Id.ToString().Contains(SearchItem)
                    )
                    .ToList();
            }
            else
            {
                throw new Exception("Database connection failed");
            }
        }
    }
}
