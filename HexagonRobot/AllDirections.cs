using System;
using System.Collections.Generic;
using System.Text;

namespace HexagonRobot
{
    /// <summary>
    /// A Class representing the set of directions.
    /// </summary>
    public class AllDirections
    {
        private static Directions _direction;
        public enum Directions { North = 1, East = 2, South = 3, West = 4 };

        public static Directions Direction
        {
            get { return _direction; }
            set
            {
                switch ((int)value)
                {
                    case 1:
                        _direction = Directions.North;
                        break;
                    case 2:
                        _direction = Directions.East;
                        break;
                    case 3:
                        _direction = Directions.South;
                        break;
                    case 4:
                        _direction = Directions.West;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
