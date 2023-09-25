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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbConext _db;
        public ShoppingCartRepository(ApplicationDbConext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart obj)
        {
            _db.shoppingCarts.Update(obj);
        }
    }
}
