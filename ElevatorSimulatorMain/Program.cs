using ElevatorSimulatorModels;

namespace ElevatorSimulatorMain;
class Program
{
    static void Main(string[] args)
    {
        //initialize an array of 10 floors
        int[] floors = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        //initialize elevator requests pool. An elevator request is represented by a floor number
        List<ElevatorRequest> requests = new List<ElevatorRequest>();

        //assuming a maximum of 4 elevators, initialize 4 elevators
        Elevator[] elevators = new Elevator[4];

        for (int i = 0; i < 4; i++)
        {
            elevators[i] = new Elevator(i, 0, 10, 1);
        }

        //requests.Count > 0;

        while (true)
        {
            //get the next request
            Console.WriteLine("Enter the floor number you are on: ");
            int originFloor = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the floor number you want to go to: ");
            int destinationFloor = Convert.ToInt32(Console.ReadLine());
       
            ElevatorRequest elevatorRequest = new ElevatorRequest
            {
                OriginFloor = originFloor,
                DestinationFloor = destinationFloor
            };
            //add the request to the list
            requests.Add(elevatorRequest);

            ////get the next request
            //int request = requests[0];
            ////get the next elevator
            //Elevator elevator = elevators[0];
            ////move the elevator to the request
            //elevator.MoveToFloor(request);
            ////remove the request from the list
            //requests.Remove(request);
            foreach (Elevator elevator in elevators)
            {
                elevator.SetCurrentRequests(requests);
            //    if (elevator.CurrentFloor == request)
            //    {
            //        Console.WriteLine("Elevator is already on this floor.");
            //        break;
            //    }
            //    else
            //    {
            //        elevator.MoveToFloor(request);
            //    }
            }
        }
    }
}