namespace ElevatorSimulatorDomain
{
    public class Elevator
    {
        private int _currentFloor;
        private int _destinationFloor;
        private List<int> _destinationFloors;
        private List<ElevatorRequest> _totalCurrentRequests = new List<ElevatorRequest>();
        private List<ElevatorRequest> _thisElevatorRequests = new List<ElevatorRequest>();
        private int _elevatorId;
        private int _elevatorStatus;

        //elevator direction: 1 = up, -1 = down, 0 = idle
        private int _elevatorDirection;
        private int _elevatorMaximumCapacity;
        private int _elevatorCurrentCapacity;

        bool _elevatorDoorOpen = false;
        bool _elevatorDoorClosed;
        bool _elevatorIsMoving = false;
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
            _elevatorDoorOpen = true;
        }

        public void CloseDoor()
        {
            _elevatorDoorOpen = false;
        }

        public void MoveUp()
        {
            _elevatorIsMoving = true;
            _elevatorDirection = 1;
            _currentFloor = _currentFloor + _elevatorDirection;
        }

        public void MoveDown()
        {
            _elevatorIsMoving = true;
            _elevatorDirection = -1;
            _currentFloor = _currentFloor + _elevatorDirection;
        }

        public void MoveToFloor(int floor)
        {
            _destinationFloor = floor;
        }

        public void Stop()
        {
            _elevatorIsMoving = false;
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
            if (_elevatorDirection == 1) // if elevator is moving up
            {
                _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor == _currentFloor && x.DestinationFloor > _currentFloor).ToList();
            }
            else if (_elevatorDirection == -1) // if elevator is moving down
            {
                _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor == _currentFloor && x.DestinationFloor < _currentFloor).ToList();
            }

            foreach (ElevatorRequest request in _potentialRequests) //add passengers to elevator if there is space
            {
                if (_elevatorCurrentCapacity < _elevatorMaximumCapacity)
                {
                    _thisElevatorRequests.Add(request);
                    _totalCurrentRequests.Remove(request);
                    _elevatorCurrentCapacity++;
                }
                else
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
            // if this elevator has passengers destined for the current floor, or there is space in the elevator
            if (_thisElevatorRequests.Where(x => x.DestinationFloor == _currentFloor).ToList().Count > 0 || _elevatorCurrentCapacity < _elevatorMaximumCapacity)
            {
                Stop();
                OpenDoor();
                DropOffPassengers();
                PickUpPassengers();
                CloseDoor();
                if (_elevatorDirection == 1) //continue moving up
                {
                    MoveUp();
                }
                else if (_elevatorDirection == -1) //continue moving down
                {
                    MoveDown();
                }
            }
            else if (_elevatorDirection == 1 && _elevatorCurrentCapacity == _elevatorMaximumCapacity) //if elevator is full continue moving up
            {
                MoveUp();
            }
            else if (_elevatorDirection == -1 && _elevatorCurrentCapacity == _elevatorMaximumCapacity) //if elevator is full continue moving down
            {
                MoveDown();
            }
        }
    }
}