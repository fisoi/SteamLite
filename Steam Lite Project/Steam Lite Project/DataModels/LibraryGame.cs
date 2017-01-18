using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Lite_Project.DataModels
{
    class LibraryGame
    {
        public string UID;
        public string GID;
        public bool installed;
        public string hoursPlayed;

        public LibraryGame(string UID, string GID, bool installed, string hoursPlayed)
        {
            this.UID = UID;
            this.GID = GID;
            this.installed = installed;
            this.hoursPlayed = hoursPlayed;
        }
    }
}
