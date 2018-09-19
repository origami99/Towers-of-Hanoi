using System;

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
            // Set intial settings

            input.Get();

            // 1. Invokes the puzzle solver
            
            // 2. Invokes the visualisation
        }
    }
}
