using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBud.Backend
{
    public static class UserContext
    {
        public static bool IsLoggedIn { get; set; } = false;
        public static int SessionUserId { get; set; }
    }
}
