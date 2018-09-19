using System;
using TowersOfHanoi.Globals;

namespace TowersOfHanoi.Core
{
    public class Engine
    {
        private static Engine instance;
        private Input input;

        private Engine()
        {
            this.input = new Input();
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

            input.ReadInt();

            // 1. Invokes the puzzle solver
            
            // 2. Invokes the visualisation
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
