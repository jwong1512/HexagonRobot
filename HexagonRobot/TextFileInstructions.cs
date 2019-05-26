using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace HexagonRobot
{
    /// <summary>
    /// A Class that contains methods related to performing commands through a Text File.
    /// </summary>
    public class TextFileInstructions
    {
        /// <summary>
        /// This method loops through the instructions in a Text File and then executes them one by one.
        /// </summary>
        /// <param name="_robot">The Robot Object.</param>
        /// <param name="_table">The Table Object.</param>
        /// <param name="_spaces">The number of spaces the Robot moves.</param>
        /// <returns></returns>
        public static bool ExecuteTextFileInstructions(Robot _robot, Table _table, int _spaces)
        {
            var _logger = new Logger();
            string instructionFilePath = System.IO.Directory.GetCurrentDirectory() + @"\InputFile\Instructions.txt";
            try
            {
                using (var reader = new StreamReader(instructionFilePath))
                {
                    _logger.Log(Logger.LogLevel.START, "Start executing Text File Instructions. Instructions located at: " + instructionFilePath);
                    string[] lines = reader.ReadToEnd().Split("\n");
                    foreach (var item in lines)
                    {
                        var command = item;
                        if (Actions.GetInput(item) == "PLACE")
                        {
                            string[] commands = Actions.ReturnPlaceArray(_robot, item);
                            var direction = (AllDirections.Directions)(int.Parse(commands[2]));
                            _robot.SetPosition(Table.TableSize - int.Parse(commands[0]) - 1, int.Parse(commands[1]), direction);
                            _logger.Log(Logger.LogLevel.START, "Start setting new location with PLACE COMMAND: " + commands[0] + ", " + commands[1] + ", " + commands[2]);
                            Console.Clear();
                            Console.WriteLine();
                            Messages.NewLocation(_robot);
                            _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                            _logger.Log(Logger.LogLevel.END, "End setting New Location.");
                        }
                        else if (Actions.GetInput(command) == "MOVE")
                        {
                            _logger.Log(Logger.LogLevel.START, "Start to MOVE robot in direction " + _robot.direction);
                            if (!_robot.Move(_robot.direction, _spaces))
                            {
                                Messages.InvalidMove(_robot.direction);
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine();
                                _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                            }
                            _logger.Log(Logger.LogLevel.END, "End Text File Instruction to MOVE robot.");
                        }
                        else if (Actions.GetInput(command) == "LEFT") Actions.TurnLeft(_robot, _table);
                        else if (Actions.GetInput(command) == "RIGHT") Actions.TurnRight(_robot, _table);
                        else if (Actions.GetInput(command) == "REPORT") Actions.ReportPosition(_robot, _table);
                        else if (command.ToUpper() == "QUIT") break;
                    }
                }
                _logger.Log(Logger.LogLevel.END, "End executing Text File Instructions.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// This method loops through the instructions of the Text File and then
        /// uses the ValidateTextFile method to check that each command is a 
        /// valid command.
        /// </summary>
        /// <returns>Returns a true or false depending on whether every instruction
        /// is valid or not.</returns>
        public static bool CheckTextFileInstructions()
        {
            var _logger = new Logger();
            string instructionFilePath = System.IO.Directory.GetCurrentDirectory() + @"\InputFile\Instructions.txt";
            try
            {
                _logger.Log(Logger.LogLevel.START, "Start checking if text file instructions are valid instructions. Instructions located at: " + instructionFilePath);
                using (var reader = new StreamReader(instructionFilePath))
                {
                    string[] lines = reader.ReadToEnd().Split("\n");
                    foreach (var item in lines)
                    {
                        if (!ValidateTextFile(item))
                            return false;
                    }
                }
                _logger.Log(Logger.LogLevel.END, "Finished Checking Text File Instructions.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// This method validates each command that is passed to it to check they are valid
        /// commands.
        /// </summary>
        /// <param name="input">The command to validate.</param>
        /// <returns>Returns a true or false depending on whether the command is valid or not.</returns>
        public static bool ValidateTextFile(string input)
        {
            var _logger = new Logger();
            try
            {
                string pattern = @"([Pp][Ll][Aa][Cc][Ee] [0-9],\s?[0-9],\s?([Nn][Oo][Rr][Tt][Hh]|[Ss][Oo][Uu][Tt][Hh]|[Ee][Aa][Ss][Tt]|[Ww][Ee][Ss][Tt]))|([Mm][Oo][Vv][Ee])|([Ll][Ee][Ff][Tt])|([Rr][Ii][Gg][Hh][Tt])|([Rr][Ee][Pp][Oo][Rr][Tt])";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(input);
                if (match.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Logger.LogLevel.ERROR, ex.StackTrace);
                throw;
            }
        }
    }
}
