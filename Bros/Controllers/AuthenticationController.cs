using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bros.DataModel;

namespace Bros.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Index()
        {
            string password = this.Request["password"];
            string username = this.Request["username"];
            HomeController controller = new HomeController();
            User user = new User();
            if (Login(password, username, out user))
            {
                Session["user"] = user;
                return View("Home/Index");
            }

            return View("Home/Index");
        }

        private bool Login(string password, string username, out User user)
        {
            bool isLoggedIn = false;
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                user = ((IEnumerable<User>)context.Users.Where(n => n is User && ((User)n).Email == username)).First();
                byte[] enteredPassword = GeneratedSaltedHash(password, user.Salt);
                if (CompareByteArrays(enteredPassword, user.Password))
                {
                    Session["user"] = user;
                    isLoggedIn = true;
                }

            }
            return isLoggedIn;
        }

        public static byte[] GeneratedSaltedHash(string plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] plainTextWithSaltedBytes = new byte[plainTextBytes.Length + salt.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
            {
                plainTextWithSaltedBytes[i] = plainTextBytes[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltedBytes[i + plainTextBytes.Length] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltedBytes);
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        public static byte[] CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[size];
            rng.GetBytes(salt);

            return salt;
        }

    }
}
