using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCPEntetities
{
    public partial class OwnersInformation
    {

        public TicketStatus Status
        {
            get {
                if (exp != null)
                    return TicketStatus.Gone;
                if (Activities.Any(a => a.ReturnTime == null))
                    return TicketStatus.OnTheGo;
                return TicketStatus.Present;
            }
        }

    }

    public enum TicketStatus
    {
        Present,
        [Display(Name="On The Go")]
        OnTheGo,
        Gone
    }
}