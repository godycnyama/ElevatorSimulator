using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ElevatorSimulatorDomain;

namespace ElevatorSimulatorUnitTests
{
    [TestClass]
    public class ElevatorTests
    {
        public ElevatorTests()
        {

        }
        [TestMethod]
        public void TestMoveUp()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 10, 1);

            // Act
            elevator.MoveUp();

            // Assert
            Assert.AreEqual(2, elevator.GetCurrentFloor());
        }

        [TestMethod]
        public void TestMoveDown()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 10, 3);

            // Act
            elevator.MoveDown();

            // Assert
            Assert.AreEqual(2, elevator.GetCurrentFloor());
        }

        [TestMethod]
        public void TestMoveToFloor()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 10, 1);

            // Act
            elevator.MoveToFloor(5);

            // Assert
            Assert.AreEqual(5, elevator.GetCurrentFloor());
        }

        [TestMethod]
        public void TestPickUpPassengers()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 5, 1);
            List<ElevatorRequest> requests = new List<ElevatorRequest>
        {
            new ElevatorRequest(1, 4),
            new ElevatorRequest(2, 5),
            new ElevatorRequest(3, 3)
        };
            elevator.SetCurrentRequests(requests);

            // Act
            elevator.PickUpPassengers();

            // Assert
            Assert.AreEqual(3, elevator.GetCurrentCapacity());
            Assert.AreEqual(2, elevator.GetDestinationFloors().Count);
        }

        [TestMethod]
        public void TestDropOffPassengers()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 5, 5, 1);
            List<ElevatorRequest> requests = new List<ElevatorRequest>
        {
            new ElevatorRequest(1, 4),
            new ElevatorRequest(2, 5),
            new ElevatorRequest(3, 3)
        };
            elevator.SetCurrentRequests(requests);

            // Act
            elevator.PickUpPassengers();
            elevator.ExecuteMove();

            // Assert
            Assert.AreEqual(0, elevator.GetCurrentCapacity());
            Assert.AreEqual(0, elevator.GetDestinationFloors().Count);
        }
    }
}