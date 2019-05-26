using System;
using System.Collections.Generic;
using System.Text;

namespace HexagonRobot
{
    public class RunRobot
    {
        private int _option;
        private Robot _robot;
        private bool _quit;
        private Table _table;
        private int _spaces;
        private readonly Logger _logger;

        public RunRobot(Logger logger)
        {
            _logger = logger;
            _robot = new Robot();
            _table = new Table();
            _quit = false;
            _spaces = 1;
        }

        /// <summary>
        /// This method is the Starting Point and main guts of the application. It is 
        /// here that the application will continuously ask the User to select the options
        /// and type in the commands until the User finally decides to quit the application.
        /// </summary>
        public void Loop()
        {
            Messages.WelcomeScreen();
            Actions.SettingInitialPlace(_robot, _table);
            do
            {
                Console.Clear();                            // prepare screen for next display
                Console.WriteLine(Messages.Message);   // display any error message (or blank if no error message);
                Messages.Message = "";
                _table.DrawTable(_robot.PositionX, _robot.PositionY, _robot.RobotSymbol);
                Messages.Instructions();
                Console.Write("\nSelect your option: ");
                if (int.TryParse(Console.ReadLine(), out _option))
                {
                    if (_option == 1) Actions.MoveForward(_robot, _table, _spaces);
                    else if (_option == 2) Actions.TurnLeft(_robot, _table);
                    else if (_option == 3) Actions.TurnRight(_robot, _table);
                    else if (_option == 4) Actions.ReportPosition(_robot, _table);
                    else if (_option == 5) Actions.TypeCommand(_robot, _table, _spaces);
                    else if (_option == 6)
                    {
                        if (TextFileInstructions.CheckTextFileInstructions())
                        {
                            TextFileInstructions.ExecuteTextFileInstructions(_robot, _table, _spaces);
                        }
                        else
                        {
                            Messages.InvalidTextFileInstructions();
                        }
                    }
                    else if (_option == 7) _quit = true;
                    else
                        Messages.InvalidInput();
                }
                else
                {
                    Messages.InvalidInput();
                }
            } while (!_quit);
        }

        
    }
}
