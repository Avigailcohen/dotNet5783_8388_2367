using BIImplementation;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIApi
{
   public class Factory
    {
        public static IBl? Get()
        {

            return new Bl();
        }
    }
}
