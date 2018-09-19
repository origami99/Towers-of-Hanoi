using System;
using TowersOfHanoi.Globals;
using TowersOfHanoi.Visualization;

namespace TowersOfHanoi.Core
{
    public class Engine
    {
        private static Engine instance;
        private ViewConsole visualisation;

        private Engine()
        {
            this.visualisation = new ViewConsole();
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
            IntilizeSettings();

            Input.ReadInt();

            // 1. Invokes the puzzle solver

            // 2. Invokes the visualisation
            visualisation.Print();
        }

        public void IntilizeSettings()
        {
            Console.Title = "Towers of Hanoi";

            Console.WindowWidth = Constants.CONSOLE_WIDTH;
            Console.BufferWidth = Constants.CONSOLE_WIDTH;

            Console.WindowHeight = Constants.CONSOLE_HEIGHT;
            Console.BufferHeight = Constants.CONSOLE_HEIGHT;
        }
    }
}
