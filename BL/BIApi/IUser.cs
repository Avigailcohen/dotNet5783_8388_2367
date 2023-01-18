using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIApi
{
    public  interface IUser
    {
        BO.User LogIn(string username, string  password);
        void SignIn(BO.User user);
    }
}
