using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager goalManager = new GoalManager();
            //code to load savedata
            goalManager.LoadData("savedata.txt");
            Menu.DisplayMainMenu(goalManager);
            //code to save to SaveData
            goalManager.SaveData("savedata.txt");
        }
    }
}
