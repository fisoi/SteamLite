using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Lite_Project.DataModels
{
    class User
    {
        public string UID;
        public string CID;
        public string username;
        public string password;
        public string profileName;
        public string email;

        public User(string UID, string CID, string username, string password, string profileName, string email)
        {
            this.UID = UID;
            this.CID = CID;
            this.username = username;
            this.password = password;
            this.profileName = profileName;
            this.email = email;
        }
    }
}
