using System;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Globals;
using TowersOfHanoi.Visualization;

namespace TowersOfHanoi.Core
{
    public class Engine
    {
        private static Engine instance;

        private Engine()
        {
        }

        public static Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    return instance = new Engine();
                }

                return instance;
            }
        }

        public void Start()
        {
            // Set intial console settings
            IntilizeSettings();

            // Read input
            Input.ReadInt();

            // 1. Invokes the puzzle solver
            var test = new SolverRecursive(LocalDataBase.DiskCounts);
            test.Execute();

            // Invokes the visualisation
            ViewConsole.Print();

            Console.ReadKey(true);
        }

        public void IntilizeSettings()
        {
            Console.Title = "Towers of Hanoi";

            // Console.CursorVisible = false;

            Console.WindowWidth = Constants.CONSOLE_WIDTH;
            Console.BufferWidth = Constants.CONSOLE_WIDTH;

            Console.WindowHeight = Constants.CONSOLE_HEIGHT;
            Console.BufferHeight = Constants.CONSOLE_HEIGHT;
        }
    }
}
