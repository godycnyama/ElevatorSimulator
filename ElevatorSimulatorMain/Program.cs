using ElevatorSimulatorDomain;

namespace ElevatorSimulatorMain;
class Program
{
    static void Main(string[] args)
    {
        ElevatorControlSystem elevatorControlSystem = new ElevatorControlSystem();
        elevatorControlSystem.RunElevatorSystem();
    }
}