using Photos.DataAccess.Data;
using Photos.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photos.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbConext _db;
        public ICategoryRepository Category { get; private set; }
        public UnitOfWork(ApplicationDbConext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }
        
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
