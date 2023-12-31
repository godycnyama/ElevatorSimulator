﻿namespace ElevatorSimulatorDomain
{
    public class Elevator
    {
        private int _currentFloor;
        private List<ElevatorRequest> _totalCurrentRequests = new List<ElevatorRequest>();
        private List<ElevatorRequest> _thisElevatorRequests = new List<ElevatorRequest>();
        private readonly int _elevatorId;

        //elevator direction: 1 = up, -1 = down
        private int _elevatorDirection;
        private int _elevatorMaximumCapacity;
        private int _elevatorCurrentCapacity;

        bool _elevatorDoorOpen = false;
        bool _elevatorIsMoving = false;
        public Elevator(int elevatorId, int elevatorCurrentCapacity, int elevatorMaximumCapacity, int currentFloor)
        {
            _currentFloor = currentFloor;
            _elevatorId = elevatorId;
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

        public void Stop()
        {
            _elevatorIsMoving = false;
        }

        public int GetCurrentFloor()
        {
            return _currentFloor;
        }

        public int GetCurrentCapacity()
        {
            return _elevatorCurrentCapacity;
        }

        public int GetElevatorId()
        {
            return _elevatorId;
        }

        public bool GetElevatorDoorOpen()
        {
            return _elevatorDoorOpen;
        }

        public bool GetElevatorIsMoving()
        {
            return _elevatorIsMoving;
        }

        public void SetThisElevatorRequests(List<ElevatorRequest> thisElevatorRequests)
        {
            _thisElevatorRequests = thisElevatorRequests;
            _elevatorCurrentCapacity = _thisElevatorRequests.Count;
        }

        public List<ElevatorRequest> GetThisElevatorRequests()
        {
            return _thisElevatorRequests;
        }

        public void SetCurrentRequests(List<ElevatorRequest> currentRequests)
        {
            _totalCurrentRequests = currentRequests;
        }

        public void PickUpPassengers()
        {
            if (_elevatorDirection == 0)
            {
                Console.WriteLine("Elevator " + _elevatorId + " is not moving");
            }
            int _numberOfPassengersToPickUp = _elevatorMaximumCapacity - _elevatorCurrentCapacity;
            int _numberOfPassengersWaiting = _totalCurrentRequests.Count(x => x.OriginFloor == _currentFloor);
            int _numberOfPassengersPickedUp = 0;

            List<ElevatorRequest> _potentialRequests = new List<ElevatorRequest>();
            List<ElevatorRequest> _requestsUp = new List<ElevatorRequest>();
            _requestsUp = _totalCurrentRequests.Where(x => x.OriginFloor == _currentFloor && x.DestinationFloor > _currentFloor).ToList();
            List<ElevatorRequest> _requestsDown = new List<ElevatorRequest>();
            _requestsDown = _totalCurrentRequests.Where(x => x.OriginFloor == _currentFloor && x.DestinationFloor < _currentFloor).ToList();

            if (_elevatorDirection == 1) // if elevator is moving up
            {
                Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving up");
                _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor == _currentFloor && x.DestinationFloor > _currentFloor).ToList();
            }
            else if (_elevatorDirection == -1) // if elevator is moving down
            {
                Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving down");
                _potentialRequests = _totalCurrentRequests.Where(x => x.OriginFloor == _currentFloor && x.DestinationFloor < _currentFloor).ToList();
            } 
            else if (_elevatorDirection == 0) // if elevator is not moving
            {
               if(_requestsUp.Count > _requestsDown.Count)
                {
                    _potentialRequests = _requestsUp;
                    _elevatorDirection = 1;
                    Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving up");
                }
                else if (_requestsUp.Count < _requestsDown.Count)
                {
                    _potentialRequests = _requestsDown;
                    _elevatorDirection = -1;
                    Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving down");
                }
                else if (_requestsUp.Count == _requestsDown.Count)
                {
                    _potentialRequests = _requestsUp;
                    _elevatorDirection = 1;
                    Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving up");
                }
            }

            foreach (ElevatorRequest request in _potentialRequests) //add passengers to elevator if there is space
            {
                if (_elevatorCurrentCapacity < _elevatorMaximumCapacity)
                {
                    _thisElevatorRequests.Add(request); //add request to this elevator
                    _totalCurrentRequests.Remove(request);//remove request from total requests
                    _elevatorCurrentCapacity++; //increase elevator current capacity
                    _numberOfPassengersPickedUp++;
                }
                else
                {
                    break;
                }
            }

            if (_numberOfPassengersPickedUp > 0)
            {
                Console.WriteLine("Elevator " + _elevatorId + " picked up " + _numberOfPassengersPickedUp + " passengers from floor " + _currentFloor);
            }
        }

        public void DropOffPassengers()
        {
            List<ElevatorRequest> _completedRequests = _thisElevatorRequests.Where(x => x.DestinationFloor == _currentFloor).ToList();
            _elevatorCurrentCapacity -= _completedRequests.Count;
            _thisElevatorRequests.RemoveAll(x => x.DestinationFloor == _currentFloor);
            if (_completedRequests.Count > 0)
            {
                Console.WriteLine("Elevator " + _elevatorId + " dropped off " + _completedRequests.Count + " passengers at floor " + _currentFloor);
            }
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
                    Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving up");
                    MoveUp();
                }
                else if (_elevatorDirection == -1) //continue moving down
                {
                    Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving down");
                    MoveDown();
                }
            }
            else if (_elevatorDirection == 1 && _elevatorCurrentCapacity == _elevatorMaximumCapacity) //if elevator is full continue moving up
            {
                Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving up");
                MoveUp();
            }
            else if (_elevatorDirection == -1 && _elevatorCurrentCapacity == _elevatorMaximumCapacity) //if elevator is full continue moving down
            {
                Console.WriteLine("Elevator " + _elevatorId + " is on floor " + _currentFloor + " moving down");
                MoveDown();
            }
        }
    }
}