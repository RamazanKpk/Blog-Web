using Entity.Concrate;
using System.Web;

namespace Web.Login
{
    public class Login
    {
        public static bool IsActive
        {
            get { return HttpContext.Current.Session["ActiveUser"] != null; }
        }

        public static bool IsAdmin
        {
            get { return HttpContext.Current.Session["AdminUser"] != null; }
        }

        public static User ActiveUser
        {
            get { return (User)HttpContext.Current.Session["ActiveUser"]; }
            set { HttpContext.Current.Session["ActiveUser"] = value; }
        }

        public static User AdminUser
        {
            get { return (User)HttpContext.Current.Session["AdminUser"]; }
            set { HttpContext.Current.Session["AdminUser"] = value; }
        }

        public static void SignOut()
        {
            ActiveUser = null;
            AdminUser = null;
            HttpContext.Current.Session.Abandon();
        }
    }

}