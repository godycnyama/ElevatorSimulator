namespace ElevatorSimulatorDomain
{
    public class ElevatorControlSystem
    {
        //assuming a maximum of 10 floors initialize an array of 10 floors
        private int[] _floors = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        //initialize elevator requests pool.
        private List<ElevatorRequest> _requests = new List<ElevatorRequest>();

        //assuming a maximum of 4 elevators, initialize 4 elevators
        private Elevator[] _elevators = new Elevator[4];

        public ElevatorControlSystem()
        {
            //initialize 4 elevators
            for (int i = 0; i < 4; i++)
            {
                _elevators[i] = new Elevator(i, 0, 10, 1);
            }
        }

        public void RunElevatorSystem()
        {
            while (true)
            {
                Console.WriteLine("Enter the floor number you are on: ");
                int originFloor = Convert.ToInt32(Console.ReadLine());

                //check if the origin floor is valid
                if (!_floors.Contains(originFloor))
                {
                    Console.WriteLine("You entered an invalid origin floor number. Valid floors are between {0} and {1}!", _floors[0], _floors[_floors.Length - 1]);
                    continue;
                }

                Console.WriteLine("Enter the floor number you want to go to: ");
                int destinationFloor = Convert.ToInt32(Console.ReadLine());

                //check if the destination floor is valid
                if (!_floors.Contains(destinationFloor))
                {
                    Console.WriteLine("You entered an invalid destination floor number. Valid floors are between {0} and {1}!", _floors[0], _floors[_floors.Length - 1]);
                    continue;
                }

                if (originFloor == destinationFloor)
                {
                    Console.WriteLine("You are already on floor {0}, please enter a different floor!", originFloor);
                    continue;
                }

                ElevatorRequest elevatorRequest = new ElevatorRequest(originFloor, destinationFloor);

                //add the request to the list
                _requests.Add(elevatorRequest);

                foreach (Elevator elevator in _elevators)
                {
                    elevator.SetCurrentRequests(_requests);
                    elevator.ExecuteMove();
                }
            }
        }
    }
}
