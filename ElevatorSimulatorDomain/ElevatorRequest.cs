namespace ElevatorSimulatorDomain
{
    public class ElevatorRequest
    {
        public int OriginFloor { get; set; }
        public int DestinationFloor { get; set; }
        public ElevatorRequest(int originFloor, int destinationFloor)
        {
            OriginFloor = originFloor;
            DestinationFloor = destinationFloor;
        }
    }
}
