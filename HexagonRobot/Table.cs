using System;
using System.Collections.Generic;
using System.Text;

namespace HexagonRobot
{
    /// <summary>
    /// This class represents the template for creating an Instance of the Table Object.
    /// </summary>
    public class Table
    {
        public const int TableSize = 5; // default size
        public const string UsedSpace = " O";
        public const string TableSymbol = " .";

        public Table()
        {
            TableArray = new string[TableSize, TableSize];
            InitTable();
        }

        /// <summary>
        /// The method that creates the Table where the Robot walks
        /// across.
        /// </summary>
        public static string[,] TableArray { get; set; } // array we will be updating with our move

        public void InitTable()
        {
            for (int i = 0; i < TableSize; i++)
            {
                for (int c = 0; c < TableSize; c++)
                {
                    TableArray[i, c] = TableSymbol;
                }
            }
        }

        /// <summary>
        /// This method draws the table and draws the robot, which is represented by
        /// the character X, on the table.
        /// </summary>
        /// <param name="posX">The x-coordinate.</param>
        /// <param name="posY">The y-coordinate.</param>
        /// <param name="robot">The robot represented by the RobotSymbol.</param>
        public void DrawTable(int posX, int posY, string robot)
        {
            Console.WriteLine();
            for (int i = 0; i < TableSize; i++)
            {
                for (int c = 0; c < TableSize; c++)
                {
                    if (i == posX && c == posY)
                    {
                        Console.Write(robot);
                    }
                    else
                    {
                        Console.Write(TableArray[i, c]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
