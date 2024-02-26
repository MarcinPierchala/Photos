using Photos.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photos.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        void UpdateStatus(int it, string orderStatus, string? paymentStatus = null);
        void UpdateSrtipePaymentID(int id, string sessionId, string paymentIntentId);
    }   
}
