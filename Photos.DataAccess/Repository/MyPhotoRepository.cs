using Photos.DataAccess.Data;
using Photos.DataAccess.Repository.IRepository;
using Photos.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Photos.DataAccess.Repository
{
    public class MyPhotoRepository : Repository<MyPhoto>, IMyPhotoRepository
    {
        private ApplicationDbConext _db;
        public MyPhotoRepository(ApplicationDbConext db) : base(db)
        {
            _db = db;
        }

        public void Update(MyPhoto obj)
        {
            _db.MyPhotos.Update(obj);
        }
    }
}
