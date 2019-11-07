using OfficialCPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OfficialCPP.Controllers
{
    public class OverDueController: ApiController, IDisposable
    {

       private ValetLogEntities1 db = new ValetLogEntities1();
        public IEnumerable<int> Get(int? id)
        {
            //Default checktime
            var overduetime = id ?? 300;
            var overdue = DateTime.Now.AddSeconds(overduetime);

            return db.Activities.Where(a => a.TimeRequested < overdue && a.TimeLeft == null).Select(b=>b.OwnersInformation.TicketNumber).ToList();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}