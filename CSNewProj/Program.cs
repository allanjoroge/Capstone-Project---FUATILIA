using OfficialCPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNewProj
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                using (var db = new ValetLogEntities1())
                {
                    var overDueTickets = db.Activities.Where(a => a.TimeRequested < DateTime.Now.AddMinutes(-5) && a.TimeLeft == null);
                    foreach(var Overdue in overDueTickets)
                    {
                        
                        Console.WriteLine("Hey is this ticket gone");
                    }
                    Console.WriteLine();

                }
            }

            

        }
    }
}
