using System;
using System.Collections.Generic;
using System.Text;
using TowersOfHanoi.Globals;

namespace TowersOfHanoi.Core
{
    public class ConsoleInterfaceSettings :IApplicationSettings
    {

        public void IntilizeSettings()
        {
            Console.Title = "Towers of Hanoi";

            Console.CursorVisible = false;

            Console.WindowWidth = Constants.CONSOLE_WIDTH;
            Console.BufferWidth = Constants.CONSOLE_WIDTH;

            Console.WindowHeight = Constants.CONSOLE_HEIGHT;
            Console.BufferHeight = Constants.CONSOLE_HEIGHT;
        }
    }
}
