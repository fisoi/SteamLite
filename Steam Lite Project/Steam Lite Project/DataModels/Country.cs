using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Lite_Project.DataModels
{
    class Country
    {
        public int ID;
        public string name;

        public Country(int ID, string name)
        {
            this.ID = ID;
            this.name = name;
        }
    }
}
