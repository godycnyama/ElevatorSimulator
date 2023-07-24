using ElevatorSimulatorDomain;

namespace ElevatorSimulatorMain;
class Program
{
    static void Main(string[] args)
    {
        //assuming a maximum of 10 floors initialize an array of 10 floors
        int[] floors = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        //initialize elevator requests pool. 
        List<ElevatorRequest> requests = new List<ElevatorRequest>();

        //assuming a maximum of 4 elevators, initialize 4 elevators
        Elevator[] elevators = new Elevator[4];

        for (int i = 0; i < 4; i++)
        {
            elevators[i] = new Elevator(i, 0, 10, 1);
        }

        while (true)
        {
            //get the next request
            Console.WriteLine("Enter the floor number you are on: ");
            int originFloor = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the floor number you want to go to: ");
            int destinationFloor = Convert.ToInt32(Console.ReadLine());

            if(originFloor == destinationFloor)
            {
                Console.WriteLine("You are already on floor {0}, please enter a different floor!", originFloor);
                continue;
            }

            ElevatorRequest elevatorRequest = new ElevatorRequest(destinationFloor, originFloor);
            
           
            //add the request to the list
            requests.Add(elevatorRequest);

            foreach (Elevator elevator in elevators)
            {
                elevator.SetCurrentRequests(requests);
                elevator.ExecuteMove();
            }
        }
    }
}