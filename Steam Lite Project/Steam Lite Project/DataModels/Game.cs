using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Lite_Project.DataModels
{
    class Game
    {
        public int GID;
        public int SID;
        public int PID;
        public string title;
        public string description;
        public float price;
        public string releaseDate;
        public int reviewsAmount;
        public float reviewScore;

        public Game(int GID, int SID, int PID, string title, string description, float price, string releaseDate, int reviewsAmount, float reviewScore)
        {
            this.GID = GID;
            this.SID = SID;
            this.PID = PID;
            this.title = title;
            this.description = description;
            this.price = price;
            this.releaseDate = releaseDate;
            this.reviewsAmount = reviewsAmount;
            this.reviewScore = reviewScore;
        }
    }
}
