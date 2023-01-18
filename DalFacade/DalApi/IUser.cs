﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IUser : ICrud<User>
    {
        public void Add(User user);
        public User GetByUserName(string userName);
    }
}
