using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        public string userName { get; set; }
        public string password { get; set; }
        public Status status { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
