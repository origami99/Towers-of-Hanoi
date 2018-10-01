using System;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Globals;
using TowersOfHanoi.Visualization;

namespace TowersOfHanoi.Core
{
    public class Engine : IEngine
    {
        private readonly IApplicationSettings appSetings;
        private readonly ISolver solver;
        private readonly IInputData inputData;
        private readonly IVisualization visualization;
        private  IDataBase dataBase;

        public Engine(IApplicationSettings appSetings,
            ISolver solver,
            IInputData inputData,
            IVisualization visualization,
            IDataBase dataBase)
        {
            this.appSetings = appSetings;
            this.solver = solver;
            this.inputData = inputData;
            this.visualization = visualization;
            this.dataBase = dataBase;
        }

        public void Start()
        {
            // Set initial console settings
            this.appSetings.IntilizeSettings();

            // Read input
            this.inputData.ReadInt();
            this.dataBase.DiskCounts = inputData.DiscsCount;

            // Invokes the puzzle solver
            this.solver.Execute();

            // Invokes the visualization
            this.visualization.Print();

            // TODO disconnect from Console
            Console.ReadKey(true);
        }
    }
}
