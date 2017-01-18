using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Lite_Project.DataModels
{
    class Publisher
    {
        public int PID;
        public string publisherName;
        public string companyName;
        public string description;

        public Publisher(int PID, string publisherName, string companyName, string description)
        {
            this.PID = PID;
            this.publisherName = publisherName;
            this.companyName = companyName;
            this.description = description;
        }
    }
}
