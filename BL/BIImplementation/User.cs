using BIApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BIImplementation
{
    internal class User:IUser
    {
        DalApi.IDal? dal = DalApi.Factory.Get();
        public  BO.User LogIn(string username, string  password)
       {
            DO.User user;
            try
            {
                user=dal!.User.GetByUserName(username);
                if (user.password != password)
                    throw new BO.BlIncorrectDateException("Worng Password");

            }
            catch(DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException(ex.Message);
            }
            return new BO.User
            {
                userName= username,
                password=password,
                status=(BO.Status)user.Status
            };
            
       }
        public void SignIn(BO.User user)
        {
            try
            {
                dal.User.Add(new DO.User { userName= user.userName,password= user.password , Status = (DO.Status)user.status });
            }
            catch(DO.DalIdAlreadyExistException ex) {throw new BO.BlIdAlreadyExistException("Already exist" ); }
        }

    }
}
