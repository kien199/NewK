using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baochi
{
    [Serializable]
    public class UserLogin
    {
        public int userID { set; get; }
        public string userName { set; get; }
    }
}