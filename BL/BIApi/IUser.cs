using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIApi
{
    public  interface IUser
    {/// <summary>
    /// function which checj the detalis of the user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
        BO.User LogIn(string username, string  password);
        /// <summary>
        /// ----- deletethis function (לא עושה כלום)
        /// </summary>
        /// <param name="user"></param>
        void SignIn(BO.User user);
    }
}
