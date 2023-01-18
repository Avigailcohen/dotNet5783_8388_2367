using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalUser : IUser
    {
        readonly string s_user = "users";
        public void Add(User user)
        {
            List<DO.User?> listUsers = XmlTools.LoadListFromXMLSerializer<DO.User>(s_user);
            if (listUsers.Exists(x => x?.userName == user.userName))
                throw new DalIdAlreadyExistException("Exist");
            listUsers.Add(user);
            XmlTools.SaveListToXMLSerializer(listUsers, "users");



        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User?> GetAll(Func<User?, bool>? filter = null)
        {
            List<DO.User?> listUsers = XmlTools.LoadListFromXMLSerializer<DO.User>(s_user);
            if (filter == null)
                return from item in listUsers
                       select item;
            return from item in listUsers
                   where filter(item) == true
                   select item;

            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }

        int ICrud<User>.Add(User item)
        {
            throw new NotImplementedException();
        }
    }
}
