using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulatorModels
{
    public class Elevator
    {
        private int _currentFloor;
        private int _destinationFloor;
        private List<int> _destinationFloors;
        private List<ElevatorRequest> _totalCurrentRequests;
        private List<ElevatorRequest> _thisElevatorRequests;
        private int _elevatorId;
        private int _elevatorStatus;
        private int _elevatorDirection;
        private int _elevatorMaximumCapacity;
        private int _elevatorCurrentCapacity;

        bool _elevatorDoorOpen;
        bool _elevatorDoorClosed;
        public Elevator(int elevatorId, int elevatorCurrentCapacity, int elevatorMaximumCapacity, int currentFloor)
        {
            _currentFloor = 1;
            _destinationFloor = 1;
            _destinationFloors = new List<int>();
            _elevatorId = elevatorId;
            _elevatorStatus = 0;
            _elevatorDirection = 0;
            _elevatorMaximumCapacity = elevatorMaximumCapacity;
            _elevatorCurrentCapacity = elevatorCurrentCapacity;
        }

        public void OpenDoor()
        {
           _elevatorDoorOpen = true ;
        }

        public void CloseDoor()
        {
            _elevatorDoorOpen = false ;
        }

        public void MoveUp()
        {
            _elevatorDirection = 1;
            _currentFloor = _currentFloor + _elevatorDirection;
        }

        public void MoveDown()
        {
            _elevatorDirection = -1;
            _currentFloor = _currentFloor + _elevatorDirection;
        }

        public void MoveToFloor(int floor)
        {
            _destinationFloor = floor;
        }

        public void Stop()
        {
            _elevatorDirection = 0;
        }

        public void SetElevatorStatus(int status)
        {
            _elevatorStatus = status;
        }

        public int GetCurrentFloor()
        {
            return _currentFloor;
        }

        public List<int> GetDestinationFloors()
        {
            return _destinationFloors;
        }

        public void SetDestinationFloors(List<int> destinationFloors)
        {
            _destinationFloors = destinationFloors;
        }

        public void SetCurrentRequests(List<ElevatorRequest> currentRequests)
        {
            _totalCurrentRequests = currentRequests;
        }

        public void PickUpPassengers()
        {
            List<ElevatorRequest> _potentialRequests = new List<ElevatorRequest>();
            if (_elevatorDirection == 1) 
            { 
               _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor == _currentFloor && x.DestinationFloor > _currentFloor).ToList();
            }
            else if (_elevatorDirection == -1)
            {
                _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor == _currentFloor && x.DestinationFloor < _currentFloor).ToList();
            }

            foreach (ElevatorRequest request in _potentialRequests)
            {
                if(_elevatorCurrentCapacity < _elevatorMaximumCapacity)
                {
                    _thisElevatorRequests.Add(request);
                    _totalCurrentRequests.Remove(request);
                    _elevatorCurrentCapacity++;
                } else
                {
                    break;
                }   
            }
        }

        public void DropOffPassengers()
        {
            List<ElevatorRequest> _completedRequests = _thisElevatorRequests.Where(x => x.DestinationFloor == _currentFloor).ToList();
            _elevatorCurrentCapacity -= _completedRequests.Count;
            _thisElevatorRequests.RemoveAll(x => x.DestinationFloor == _currentFloor);
        }

        public void ExecuteMove()
        {
            // if there are requests for this elevator on the current floor, or there is space in the elevator, stop and open the door
            if (_thisElevatorRequests.Where(x => x.DestinationFloor == _currentFloor).ToList().Count > 0 || (_elevatorCurrentCapacity < _elevatorMaximumCapacity))
            {
                Stop();
                OpenDoor();
                DropOffPassengers();
                PickUpPassengers();
                CloseDoor();
                if(_elevatorDirection == 1)
                {
                    MoveUp();
                } else if (_elevatorDirection == -1)
                {
                    MoveDown();
                }
            }
            else if (_elevatorDirection == 1) // if the elevator is moving up, check if there are any requests for this elevator on floors above the current floor
            {
                List<ElevatorRequest> _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor > _currentFloor && x.DestinationFloor > _currentFloor).ToList();
                if (_potentialRequests.Count > 0)
                {
                    Stop();
                    OpenDoor();
                    DropOffPassengers();
                    PickUpPassengers();
                    CloseDoor();
                }
                else
                {
                    MoveUp();
                }
            }
            else if (_elevatorDirection == -1) // if the elevator is moving down, check if there are any requests for this elevator on floors below the current floor
            {
                List<ElevatorRequest> _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor < _currentFloor && x.DestinationFloor < _currentFloor).ToList();
                if (_potentialRequests.Count > 0)
                {
                    Stop();
                    OpenDoor();
                    DropOffPassengers();
                    PickUpPassengers();
                    CloseDoor();
                }
                else
                {
                    MoveDown();
                }
            }
            else if (_elevatorDirection == 0) // if the elevator is not moving, check if there are any requests for this elevator on floors above or below the current floor
            {
                List<ElevatorRequest> _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor > _currentFloor || x.OriginFloor < _currentFloor).ToList();
                if (_potentialRequests.Count > 0)
                {
                    Stop();
                    OpenDoor();
                    DropOffPassengers();
                    PickUpPassengers();
                    CloseDoor();
                }
                else
                {
                    MoveUp();
                }
            }   
            else if(_elevatorDirection == 1 && (_elevatorCurrentCapacity == _elevatorMaximumCapacity)) //if elevator is full continue moving up
            {
                MoveUp();
            }
            else if (_elevatorDirection == -1 && (_elevatorCurrentCapacity == _elevatorMaximumCapacity)) //if elevator is full continue moving down
            {
                MoveDown();
            }
        }
    }
}
