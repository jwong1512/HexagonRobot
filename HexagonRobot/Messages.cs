using System;
using System.Collections.Generic;
using System.Text;

namespace HexagonRobot
{
    /// <summary>
    /// A Class with preprepared messages that are used throughout the Application.
    /// </summary>
    public class Messages
    {
        public static string Message { get; set; }

        /// <summary>
        /// The Welcome Screen Message
        /// </summary>
        public static void WelcomeScreen()
        {
            Console.WriteLine("##########################################################################################");
            Console.WriteLine("##########################################################################################");
            Console.WriteLine("##                                                                                      ##");
            Console.WriteLine("##                           WELCOME TO THE HEXAGON ROBOT APP                           ##");
            Console.WriteLine("##                                                                                      ##");
            Console.WriteLine("##   This application allows you to control a small Robot as it travels along a table.  ##");
            Console.WriteLine("##   To begin controlling the Robot you will need to input an initial position in the   ##");
            Console.WriteLine("##   following format:                                                                  ##");
            Console.WriteLine("##                       PLACE X-coordinate, Y-coordinate, direction                    ##");
            Console.WriteLine("##                                                                                      ##");
            Console.WriteLine("##                       E.g.                                                           ##");
            Console.WriteLine("##                            PLACE 0, 0, NORTH                                         ##");
            Console.WriteLine("##                                                                                      ##");
            Console.WriteLine("##########################################################################################");
            Console.WriteLine("##########################################################################################");
        }

        /// <summary>
        /// This message lists the options available to the User.
        /// </summary>
        public static void Instructions()
        {
            Console.WriteLine();
            Console.WriteLine("[1] = MOVE [2] = LEFT [3] = RIGHT [4] = REPORT [5] TYPE COMMAND [6] INPUT FILE [7] QUIT");
        }

        /// <summary>
        /// For invalid User input.
        /// </summary>
        public static void InvalidInput()
        {
            var _logger = new Logger();
            Message = "INVALID INPUT. Input must be an integer between 1 - 7";
            _logger.Log(Logger.LogLevel.WARNING, Message);
        }

        /// <summary>
        /// For when the User invokes the Move method but will then fall off the table if the move is implemeneted.
        /// </summary>
        /// <param name="direction"></param>
        public static void InvalidMove(AllDirections.Directions direction)
        {
            var _logger = new Logger();
            Message = String.Format("INVALID MOVE. The Robot will fall off the table if you continue moving {0}", direction);
            _logger.Log(Logger.LogLevel.WARNING, Message);
        }

        /// <summary>
        /// This displays the current value of the x-coordinate, the y-coordinate and direction.
        /// </summary>
        /// <param name="PosX"></param>
        /// <param name="PosY"></param>
        /// <param name="direction"></param>
        public static void DisplayPosition(int PosX, int PosY, AllDirections.Directions direction)
        {
            Message = "Current Position:   (" + (Table.TableSize - 1 - PosX) + ", " + PosY + ", " + direction + ")";
        }

        /// <summary>
        /// This is for when the instructions listed in the Instruction.txt are not valid
        /// commands.
        /// </summary>
        public static void InvalidTextFileInstructions()
        {
            var _logger = new Logger();
            Message = "The Text File contains instructions that are not valid";
            _logger.Log(Logger.LogLevel.WARNING, Message);
        }

        /// <summary>
        /// This message is for when the Robot is out of bounds based on the coordinates provided 
        /// through a Place Command.
        /// </summary>
        /// <param name="posX">The x-coordinate provided by the User.</param>
        /// <param name="posY">The y-coordinate provided by the User.</param>
        /// <param name="direction">The direction in which the Robot is facing.</param>
        public static void RobotOutOfRangeFromPlaceCommand(string posX, string posY, string direction)
        {
            var _logger = new Logger();
            Message = "The following Place Command will result in the Robot being Out of Range: " + posX + ", " + posY + ", " + direction;
            _logger.Log(Logger.LogLevel.WARNING, Message);
        }

        /// <summary>
        /// A message that displays on the screen that the Robot has moved 1 step in the
        /// direction the Robot was facing.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="spaces"></param>
        public static void MoveForward(AllDirections.Directions direction, int spaces = 1)
        {
            Message = String.Format("The Robot has moved forward {0} step {1}", spaces, direction);
        }

        /// <summary>
        /// A message indicating that the Robot has turned Left from its current direction.
        /// </summary>
        /// <param name="direction"></param>
        public static void TurnLeft(AllDirections.Directions direction)
        {
            Message = "The Robot has turned Left and is now facing " + direction;
        }

        /// <summary>
        /// A message indicating that the Robot has turned Right from its current direction.
        /// </summary>
        /// <param name="direction"></param>
        public static void TurnRight(AllDirections.Directions direction)
        {
            Message = "The Robot has turned Right and is now facing " + direction;
        }

        /// <summary>
        /// A message indicating to the User the New Location of the Robot following a Place
        /// Command.
        /// </summary>
        /// <param name="_robot"></param>
        public static void NewLocation(Robot _robot)
        {
            Message = "You have moved to the following location:   (" + (Table.TableSize - 1 - _robot.PositionX) + ", " + _robot.PositionY + ", " + _robot.direction + ")";
        }
    }
}
