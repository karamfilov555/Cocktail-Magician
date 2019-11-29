using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Notifications.Models
{
    public class NotificationViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        //user ?
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Username { get; set; }
        public bool IsSeen { get; set; }
    }
}
