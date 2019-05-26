using System;
using System.Collections.Generic;
using System.Text;

namespace HexagonRobot
{
    /// <summary>
    /// This is a Class that acts as a template for a Robot object. It represents
    /// actions that the Robot can take and certain properties of the Robot
    /// such as its x and y coordinate and the direction its facing.
    /// </summary>
    public class Robot
    {
        public string RobotSymbol { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public AllDirections.Directions direction { get; set; }
        public Robot()
        {
            RobotSymbol = " X";
            PositionX = 0;
            PositionY = 0;
        }

        /// <summary>
        /// This method sets the details of the Robot Object based on 
        /// input provided by the User.
        /// </summary>
        /// <param name="posX">The x-coordinate</param>
        /// <param name="posY">The y-coordinate</param>
        /// <param name="_direction">The direction the Robot is facing.</param>
        /// <returns>Returns a True or False depending on whether the coordinates provided
        /// are within range or not.</returns>
        public bool SetPosition(int posX, int posY, AllDirections.Directions _direction)
        {
            if (posX < 0 || posX > Table.TableSize - 1 || posY < 0 || posY > Table.TableSize - 1)
                return false;
            PositionX = posX;
            PositionY = posY;
            direction = _direction;
            return true;
        }

        /// <summary>
        /// A method that turns the Robot anti-clockwise from the direction that
        /// it is currently facing.
        /// </summary>
        /// <param name="dir">The direction the Robot is currently facing.</param>
        /// <returns>The method returns the New direction that robot is 
        /// facing after the method is completed.</returns>
        public AllDirections.Directions Left(AllDirections.Directions dir)
        {
            switch (dir)
            {
                case AllDirections.Directions.North:
                    direction = AllDirections.Directions.West;
                    return direction;
                case AllDirections.Directions.East:
                    direction = AllDirections.Directions.North;
                    return direction;
                case AllDirections.Directions.South:
                    direction = AllDirections.Directions.East;
                    return direction;
                case AllDirections.Directions.West:
                    direction = AllDirections.Directions.South;
                    return direction;
                default:
                    return direction;
            }
        }

        /// <summary>
        /// A method that turns the Robot clockwise from the direction that
        /// it is currently facing.
        /// </summary>
        /// <param name="dir">The direction the Robot is currently facing.</param>
        /// <returns>The method returns the New direction that robot is 
        /// facing after the method is completed.</returns>
        public AllDirections.Directions Right(AllDirections.Directions dir)
        {
            switch (dir)
            {
                case AllDirections.Directions.North:
                    direction = AllDirections.Directions.East;
                    return direction;
                case AllDirections.Directions.East:
                    direction = AllDirections.Directions.South;
                    return direction;
                case AllDirections.Directions.South:
                    direction = AllDirections.Directions.West;
                    return direction;
                case AllDirections.Directions.West:
                    direction = AllDirections.Directions.North;
                    return direction;
                default:
                    return direction;
            }
        }

        /// <summary>
        /// A method that moves the Robot 1 step in the direction that 
        /// it is currently facing.
        /// </summary>
        /// <param name="direction">The direction the Robot is facing.</param>
        /// <param name="spaces">The number of spaces the Robot moves.</param>
        /// <returns></returns>
        public bool Move(AllDirections.Directions direction, int spaces)
        {
            if (ValidateMove(direction, spaces))
            {
                switch (direction)
                {
                    // If moving north or south (up or down), we only change rows (X)
                    // If moving east or west (left or right), we only change columns (Y); The row (X) remains
                    // the same
                    case AllDirections.Directions.North:
                        PositionX -= spaces;
                        return true;
                    case AllDirections.Directions.East:
                        PositionY += spaces;
                        return true;
                    case AllDirections.Directions.South:
                        PositionX += spaces;
                        return true;
                    case AllDirections.Directions.West:
                        PositionY -= spaces;
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }

        /// <summary>
        /// This method checks to see if the coordinates of the Robot is still within
        /// range after the Robot has been ordered to move.
        /// </summary>
        /// <param name="direction">The direction the Robot is facing.</param>
        /// <param name="spaces">The number of spaces the Robot moves.</param>
        /// <returns></returns>
        public bool ValidateMove(AllDirections.Directions direction, int spaces)
        {
            if (direction == AllDirections.Directions.North && (PositionX - spaces) < 0)
            {
                Messages.InvalidMove(direction);
                return false;
            }
            else if (direction == AllDirections.Directions.East && (PositionY + spaces) > Table.TableSize - 1)
            {
                Messages.InvalidMove(direction);
                return false;
            }
            else if (direction == AllDirections.Directions.South && (PositionX + spaces) > Table.TableSize - 1)
            {
                Messages.InvalidMove(direction);
                return false;
            }
            else if (direction == AllDirections.Directions.West && (PositionY - spaces) < 0)
            {
                Messages.InvalidMove(direction);
                return false;
            }
            return true;
        }
    }
}
