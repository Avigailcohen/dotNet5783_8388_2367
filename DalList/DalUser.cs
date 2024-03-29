﻿using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DalUser : IUser
    {
        public void Add(User user)
        {
            if (DataSource.usersList.Exists(x => x?.userName == user.userName))
                throw new DalIdAlreadyExistException("Exist");
            DataSource.usersList.Add(user);


        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User?> GetAll(Func<User?, bool>? filter = null)
        {
            if (filter == null)
                return from item in DataSource.usersList
                       select item;
            return from item in DataSource.usersList
                   where filter(item) == true
                   select item;

            
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByUserName(string userName)
        {
            return DataSource.usersList.FirstOrDefault(x => x?.userName == userName) ?? throw new  DalIdDoNotExistException("Not exist");
        }

        //public User GetByUserName(string userName)
        //{
        //   User user=new User();
        //    if (DataSource.usersList.Find(x => x.Value.userName == user)) ;


        //}

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
