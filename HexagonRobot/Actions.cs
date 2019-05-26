using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HexagonRobot
{
    /// <summary>
    /// This class is a set of actions that are executed from the RunRobot class. These methods assist with actioning the business logic
    /// of the application.
    /// </summary>
    public class Actions
    {
        /// <summary>
        /// The method is used to set the Initial x-coordinate, y-coordinate and
        /// direction of the robot. This method is required by the User and the User
        /// will not be able to proceed further unless a valid set of values is 
        /// provided.
        /// </summary>
        /// <param name="_robot">The Robot Object</param>
        /// <param name="_table">The Table Object</param>
        public static void SettingInitialPlace(Robot _robot, Table _table)
        {
            var _logger = new Logger();
            try
            {
                Console.WriteLine();
                Console.WriteLine("Type the Robot starting postion in 'PLACE 0,0,North' format.\n");
                Console.Write(": ");
                string instruction = Console.ReadLine();
                _logger.Log(Logger.LogLevel.START, "START setting Table Location. USER INPUT: " + instruction);
                // The ReturnPlaceArray method will either return an array with the x-coordinate, y-coordinate and 
                // direction, or it will return a null value.
                string[] instructions = ReturnPlaceArray(_robot, instruction);
                // If the instructions array is null then the following while loop will continuously prompt the 
                // User to input correct values or quit the Application.
                while (instructions == null)
                {
                    _logger.Log(Logger.LogLevel.WARNING, "INCORRECT USER INPUT: " + instruction);
                    Console.WriteLine("Type a valid PLACE Command or type 'QUIT' to exit.");
                    Console.Write(": ");
                    instruction = Console.ReadLine();
                    if (instruction.Trim().ToUpper() == "QUIT")
                    {
                        // User choosing to Quit the application.
                        _logger.Log(Logger.LogLevel.END, "User exited application.");
                        Console.WriteLine("\nYou have chosen to Exit. Good bye...");
                        Environment.Exit(0);
                    }
                    instructions = ReturnPlaceArray(_robot, instruction);
                }
                var initialDirection = (AllDirections.Directions)(int.Parse(instructions[2]));
                // The SetPosition method returns a False if the x and y coordinates are not within the range of the table.
                // It will then execute the while loop until a valid set of coordinates are chosen or until the User 
                // chooses to quit.
                while (!_robot.SetPosition(Table.TableSize - int.Parse(instructions[0]) - 1, int.Parse(instructions[1]), initialDirection))
                {
                    Console.WriteLine("New Location is out of bounds. The X-coordinate and Y-coordinate must be between 0 and " + (Table.TableSize - 1));
                    _logger.Log(Logger.LogLevel.WARNING, "Initial Location OUT OF BOUNDS. USER INPUT: (" + instructions[0] + ", " + instructions[1] + ", " + initialDirection + ")");
                    Console.WriteLine("Type a valid PLACE Command or type 'QUIT' to exit.");
                    Console.Write(": ");
                    instruction = Console.ReadLine();
                    if (instruction.Trim().ToUpper() == "QUIT")
                    {
                        _logger.Log(Logger.LogLevel.END, "User exited application.");
                        Console.WriteLine("\nYou have chosen to Exit. Good bye...");
                        Environment.Exit(0);
                    }
                    instructions = ReturnPlaceArray(_robot, instruction);
                    initialDirection = (AllDirections.Directions)(int.Parse(instructions[2]));
                }
                Messages.NewLocation(_robot);
                _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                _logger.Log(Logger.LogLevel.END, "COMPLETED setting Initial Table Location. USER INPUT: " + instruction);
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// This method is where the commands that are typed manually are processed and executed.
        /// </summary>
        /// <param name="_robot">The Robot Object.</param>
        /// <param name="_table">The Table Object.</param>
        /// <param name="_spaces">The number of spaces to move the Robot.</param>
        public static void TypeCommand(Robot _robot, Table _table, int _spaces)
        {
            var _logger = new Logger();
            try
            {
                _logger.Log(Logger.LogLevel.START, "Start typing commands");
                DisplayTypeCommandOptions();
                Console.Write(": ");
                string command = Console.ReadLine();
                // As long as the command given is not the 'Quit' command the method will keep asking
                // the User to type in commands.
                while (command.Trim().ToUpper() != "QUIT")
                {
                    // If the User inputs a 'Place' command.
                    if (GetInput(command) == "PLACE")
                    {
                        _logger.Log(Logger.LogLevel.START, "Start Place command. USER INPUT: " + command);
                        // The ReturnPlaceArray returns a null value if any invalid input is provided. This could
                        // be a null input or an input that does not meet the required set of commands.
                        string[] commands = Actions.ReturnPlaceArray(_robot, command);
                        // If the User provides an invalid input then the following while loop will keep prompting 
                        // the User until a valid input is provided or if the User 'quits'.
                        while (commands == null)
                        {
                            _logger.Log(Logger.LogLevel.WARNING, "Invalid Place Command. USER INPUT: " + commands);
                            Console.WriteLine("Invalid Input. Type a Relevant Command or type 'QUIT' to exit.");
                            Console.Write(": ");
                            command = Console.ReadLine();
                            if (command.Trim().ToUpper() == "QUIT") break;
                            commands = Actions.ReturnPlaceArray(_robot, command);
                        }
                        // If the coordinates are not within bounds then the below while loop will continue to prompt the User
                        // until valid coordinates are provided or until the User quits.
                        while (!_robot.SetPosition(Table.TableSize - _robot.PositionX - 1, Table.TableSize - _robot.PositionY - 1, _robot.direction))
                        {
                            Console.WriteLine("New Location is out of bounds. The X-coordinate and Y-coordinate must be between 0 and " + (Table.TableSize - 1));
                            _logger.Log(Logger.LogLevel.WARNING, "Initial Location OUT OF BOUNDS. USER INPUT: (" + _robot.PositionX + ", " + _robot.PositionY + ", " + _robot.direction + ")");
                            Console.WriteLine("Type a valid PLACE Command or type 'QUIT' to exit.");
                            Console.Write(": ");
                            command = Console.ReadLine();
                            if (command.Trim().ToUpper() == "QUIT")
                            {
                                _logger.Log(Logger.LogLevel.END, "User exited application.");
                                break;
                            }
                            commands = ReturnPlaceArray(_robot, command);
                        }
                        if (commands == null) break;
                        // If a valid Place command is provided and the coordinates are within range then the following will be executed.
                        if (_robot.SetPosition(Table.TableSize - int.Parse(commands[0]) - 1, int.Parse(commands[1]), (AllDirections.Directions)(int.Parse(commands[2]))))
                        {
                            Messages.NewLocation(_robot);
                            Console.Clear();
                            Console.WriteLine(Messages.Message);
                            _logger.Log(Logger.LogLevel.INFO, Messages.Message);
                            _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                        }
                        // If invalid coordinates are provided then the RobotOutOfRangeFromPlaceCommand will be executed.
                        else
                        {
                            Messages.RobotOutOfRangeFromPlaceCommand(commands[0], commands[1], commands[2]);
                            Console.Clear();
                            Console.WriteLine(Messages.Message);
                            _logger.Log(Logger.LogLevel.WARNING, Messages.Message);
                            _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                        }
                    }
                    else if (GetInput(command) == "MOVE") MoveForward(_robot, _table, _spaces);
                    else if (GetInput(command) == "LEFT") TurnLeft(_robot, _table);
                    else if (GetInput(command) == "RIGHT") TurnRight(_robot, _table);
                    else if (GetInput(command) == "REPORT") ReportPosition(_robot, _table);
                    else if (command.ToUpper() == "QUIT") break;

                    if (GetInput(command) == null)
                    {
                        Console.WriteLine("Invalid Input. Type a Relevant Command or type 'QUIT' to exit.");
                        _logger.Log(Logger.LogLevel.WARNING, "No User Input provided for Typed Command.");
                        Console.Write(": ");
                        command = Console.ReadLine();
                    }
                    else
                    {
                        Messages.Instructions();
                        DisplayTypeCommandOptions();
                        Console.Write(": ");
                        command = Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// This method returns an array of the x-coordinate, y-coordinate and direction if they are
        /// valid commands input by the User. It will return a null value if they are not.
        /// </summary>
        /// <param name="_robot">The Robot Object.</param>
        /// <param name="command">The User Input.</param>
        /// <returns></returns>
        public static string[] ReturnPlaceArray(Robot _robot, string command)
        {
            var _logger = new Logger();
            try
            {
                string pattern = @"[Pp][Ll][Aa][Cc][Ee] [0-9],\s?[0-9],\s?([Nn][Oo][Rr][Tt][Hh]|[Ss][Oo][Uu][Tt][Hh]|[Ee][Aa][Ss][Tt]|[Ww][Ee][Ss][Tt])";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(command);
                if (match.Success)
                {
                    string direction = String.Empty;
                    string pattern2 = @"(?<=^.{6})(.*)";
                    Regex regex2 = new Regex(pattern2);
                    Match newMatch = regex2.Match(match.Value);

                    string[] arr = newMatch.Value.Split(",");

                    // Assigning the Robot coordinates
                    _robot.PositionX = int.Parse(arr[0]);
                    _robot.PositionY = int.Parse(arr[1]);

                    switch (arr[2].Trim().ToUpper())
                    {
                        case "NORTH":
                            direction = "1";
                            break;
                        case "SOUTH":
                            direction = "3";
                            break;
                        case "EAST":
                            direction = "2";
                            break;
                        case "WEST":
                            direction = "4";
                            break;
                        default:
                            break;
                    }
                    return new string[] { _robot.PositionX.ToString(), _robot.PositionY.ToString(), direction };
                }
                else
                {
                    Console.WriteLine("PLACE input must be of 'PLACE 0,0,N' format");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Turns the Robot anti-clockwise from the current direction and then draws the table.
        /// </summary>
        /// <param name="_robot">The Robot Object.</param>
        /// <param name="_table">The Table Object.</param>
        public static void TurnLeft(Robot _robot, Table _table)
        {
            var _logger = new Logger();
            try
            {
                _logger.Log(Logger.LogLevel.START, "Start Turning LEFT from direction of " + _robot.direction);
                Console.Clear();
                Messages.TurnLeft(_robot.Left(_robot.direction));
                Console.WriteLine(Messages.Message);
                _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                _logger.Log(Logger.LogLevel.END, "Completed Turning LEFT");
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Turns the Robot clockwise from the current direction and then draws the table.
        /// </summary>
        /// <param name="_robot">The Robot Object.</param>
        /// <param name="_table">The Table Object.</param>
        public static void TurnRight(Robot _robot, Table _table)
        {
            var _logger = new Logger();
            try
            {
                _logger.Log(Logger.LogLevel.START, "Start Turning RIGHT from direction of " + _robot.direction);
                Console.Clear();
                Messages.TurnRight(_robot.Right(_robot.direction));
                Console.WriteLine(Messages.Message);
                _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                _logger.Log(Logger.LogLevel.END, "Completed Turning RIGHT");
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Moves the Robot forward and then Draws the table.
        /// </summary>
        /// <param name="_robot">The Robot Object.</param>
        /// <param name="_table">The Table Object.</param>
        /// <param name="_spaces">The number of spaces the Robot moves.</param>
        public static void MoveForward(Robot _robot, Table _table, int _spaces)
        {
            var _logger = new Logger();
            try
            {
                _logger.Log(Logger.LogLevel.START, "Start Moving FORWARD in direction of " + _robot.direction);
                Console.Clear();
                if (_robot.Move(_robot.direction, _spaces))
                {
                    Messages.MoveForward(_robot.direction);
                }
                Console.WriteLine(Messages.Message);
                _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                _logger.Log(Logger.LogLevel.END, "Completed Moving Forward");
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Displays on the screen the current location coordinates and location of the Robot and 
        /// then draws the table.
        /// </summary>
        /// <param name="_robot">The Robot Object.</param>
        /// <param name="_table">The Table Object.</param>
        public static void ReportPosition(Robot _robot, Table _table)
        {
            var _logger = new Logger();
            try
            {
                _logger.Log(Logger.LogLevel.START, "Start Reporting current position of (" + _robot.PositionX + ", " + _robot.PositionY + ", " + _robot.direction + ")");
                Console.Clear();
                Messages.DisplayPosition(_robot.PositionX, _robot.PositionY, _robot.direction);
                Console.WriteLine(Messages.Message);
                _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                _logger.Log(Logger.LogLevel.END, "Completed Reporting Current Position");
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Displays the Type Command options.
        /// </summary>
        public static void DisplayTypeCommandOptions()
        {
            var _logger = new Logger();
            try
            {
                Console.WriteLine("\nPlease type one of the following:\n");
                Console.WriteLine("PLACE X,Y,Direction: \tPlace the Robot in the 'X,Y' position facing 'Direction'.");
                Console.WriteLine("MOVE:\t\t\tMove one step forward in the current direction.");
                Console.WriteLine("LEFT:\t\t\tTurn Left 90 degrees from current direction.");
                Console.WriteLine("RIGHT:\t\t\tTurn Right 90 degrees from current direction.");
                Console.WriteLine("REPORT:\t\t\tDisplays the current position of the Robot.");
                Console.WriteLine("QUIT:\t\t\tExit Typing Commands and return to Options.");
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }


        /// <summary>
        /// This method is used to determine what type of command the User Input. It is either one
        /// of the valid commands or else returns a null if it isn't.
        /// </summary>
        /// <param name="input">User Input.</param>
        /// <returns></returns>
        public static string GetInput(string input)
        {
            var _logger = new Logger();
            try
            {
                string[] command = input.Split();
                if (command[0].ToUpper() == "PLACE") return "PLACE";
                else if (command[0].ToUpper() == "MOVE") return "MOVE";
                else if (command[0].ToUpper() == "LEFT") return "LEFT";
                else if (command[0].ToUpper() == "RIGHT") return "RIGHT";
                else if (command[0].ToUpper() == "REPORT") return "REPORT";
                else return null;
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }
    }
}
