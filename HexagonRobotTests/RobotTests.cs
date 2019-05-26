using System;
using Xunit;
using HexagonRobot;

namespace HexagonRobotTests
{
    public class RobotTest
    {
        private readonly Robot _robot;
        public RobotTest()
        {
            _robot = new Robot();
        }

        [Fact]
        public void SetPosition_CheckIfUserInputValuesReturnCorrectResponse_ReturnTrue()
        {
            var result = _robot.SetPosition(4, 3, AllDirections.Directions.North);
            Assert.True(result);
        }

        [Fact]
        public void SetPosition_CheckIfUserInputValuesReturnCorrectResponse_ReturnFalse()
        {
            var result = _robot.SetPosition(6, 6, AllDirections.Directions.North);
            Assert.False(result);
        }

        [Theory]
        [InlineData(AllDirections.Directions.East, AllDirections.Directions.North)]
        [InlineData(AllDirections.Directions.West, AllDirections.Directions.South)]
        [InlineData(AllDirections.Directions.North, AllDirections.Directions.West)]
        public void Left_ReturnsCorrectDirection(AllDirections.Directions direction, AllDirections.Directions expected)
        {
            var result = _robot.Left(direction);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(AllDirections.Directions.East, AllDirections.Directions.South)]
        [InlineData(AllDirections.Directions.West, AllDirections.Directions.North)]
        [InlineData(AllDirections.Directions.North, AllDirections.Directions.East)]
        public void Right_ReturnsCorrectDirection(AllDirections.Directions direction, AllDirections.Directions expected)
        {
            var result = _robot.Right(direction);
            Assert.Equal(expected, result);
        }
    }
}
