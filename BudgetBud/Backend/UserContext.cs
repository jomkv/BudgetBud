using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBud.Backend
{
    public static class UserContext
    {
        public static bool IsLoggedIn { get; set; } = false;
        public static int SessionUserId { get; set; }
        public static string UserName { get; set; } = "Username";
        public static string Status { get; set; } = "Status";
        public static string FullName { get; set; } = "Full Name";
        public static Image ProfilePic { get; set; } = null;
    }
}
