using MarsRovers;
using System;
using System.Collections.Generic;
using Xunit;


namespace MarsRover.Test
{
    public class RoverTest
    {
        [Theory]
        [InlineData(new string[] { "5 5", "1 2 N", "LMLMLMLMM"}, "1 3 N")]
        [InlineData(new string[] { "5 5", "3 3 E", "MMRMMRMRRM" }, "5 1 E")]
        public void RoverFunctionalityTest(IList<string> input, string expected)
        {
            //Arrange
            var processor = new Input();
            IRoverFunctionality roverFunctionality = new RoverFunctionality(); //TODO: Use dependency injection
             var rover = processor.ParseRover(input, roverFunctionality);

            //Act
            rover.ExecuteInstructions();
            var output = $"{rover.position.X} {rover.position.Y} {rover.position.Cardinality.ToString()}";


            Assert.NotNull(rover);
            Assert.Equal(output, expected);
        }

        //TODO: Add negative test cases

        //TODO: Add tests for small units i.e test case for TurnLeft, TurnRight and Move

        //TODO: Add tests when rover is out of bounds

        //TODO: Add tests when rovers can collide


    }
}
