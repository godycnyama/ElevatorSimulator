using ElevatorSimulatorDomain;

namespace ElevatorSimulatorUnitTests
{
    [TestClass]
    public class ElevatorTests
    {
        //test to see if the elevator has moved up
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

        //test to see if the elevator has moved down
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

        //test to see if the elevator has picked up passengers
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
            Assert.AreEqual(1, elevator.GetCurrentCapacity());
            Assert.AreEqual(1, elevator.GetThisElevatorRequests().Count);
        }


        //test to see if the elevator has dropped off passengers
        [TestMethod]
        public void TestDropOffPassengers()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 5, 4);
            List<ElevatorRequest> requests = new List<ElevatorRequest>
            {
                new ElevatorRequest(1, 4),
                new ElevatorRequest(1, 4),
                new ElevatorRequest(1, 5)
            };
            elevator.SetThisElevatorRequests(requests);

            // Act
            elevator.DropOffPassengers();

            // Assert
            Assert.AreEqual(1, elevator.GetCurrentCapacity());
            Assert.AreEqual(1, elevator.GetThisElevatorRequests().Count);
        }

        //test to see if the elevator is moving
        [TestMethod]
        public void TestIsMoving()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 5, 4);

            // Act
            elevator.MoveUp();

            // Assert
            Assert.AreEqual(true, elevator.GetElevatorIsMoving());
        }

        //test to see if the elevator is not moving
        [TestMethod]
        public void TestIsNotMoving()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 5, 4);

            // Act
            elevator.Stop();

            // Assert
            Assert.AreEqual(false, elevator.GetElevatorIsMoving());
        }

        //test to see if the elevator door is open
        [TestMethod]
        public void TestDoorOpen()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 5, 4);

            // Act
            elevator.OpenDoor();

            // Assert
            Assert.AreEqual(true, elevator.GetElevatorDoorOpen());
        }

        //test to see if the elevator door is closed
        [TestMethod]
        public void TestDoorClosed()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 5, 4);

            // Act
            elevator.CloseDoor();

            // Assert
            Assert.AreEqual(false, elevator.GetElevatorDoorOpen());
        }

        //test to see if the elevator is at the top floor
        [TestMethod]
        public void TestTopFloor()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 5, 8);

            // Act
            elevator.MoveUp();

            // Assert
            Assert.AreEqual(9, elevator.GetCurrentFloor());
        }

        //test to see if the elevator is at the bottom floor
        [TestMethod]
        public void TestBottomFloor()
        {
            // Arrange
            Elevator elevator = new Elevator(1, 0, 5, 1);

            // Act
            elevator.MoveDown();

            // Assert
            Assert.AreEqual(0, elevator.GetCurrentFloor());
        }

    }
}