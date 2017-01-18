using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Lite_Project.DataModels
{
    class GameItem
    {
        public string title;
        public float price;

        public GameItem(string title, float price)
        {
            this.title = title;
            this.price = price;
        }
    }
}
