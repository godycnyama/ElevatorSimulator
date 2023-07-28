# ElevatorSimulator

 

This is a console program that simulates an elevators control system in a multi-story building.

This is a .Net 7 project and in order to run it, one needs to install the .net 7 SDK or the .net 7 runtime.

To run it locally, clone the repo and run the dotnet restore and dotnet run commands on project ElevatorSimulatorMain.

To run the tests, enter dotnet test command.

 

## The following assumptions were used when writing this program:

#### -The building has a maximum of 10 floors

#### -The building has a maximum of 4 elevators.

#### -The elevators are identical, and each elevator has a maximum carrying capacity of 8 passengers

#### -The movement upwards is represented by 1

#### -The movement downwards is represented by -1

#### -Non movement is represented by 0

 

## How it works.

 

On any given floor, to call an elevator, waiting passengers, enter the floor they are currently (origin floor) on and the destination floor. If the current and destination floors are the same, they will be prompted to input a destination floor that is different from the origin floor.

When an elevator stops on a floor, it first drops off passengers who have destination floor the same as the current floor, then picks up passenger depending on space available. If there are no drop offs or pickups on a floor and the elevator is full, the elevator continues going whatever direction its going.

 

All elevator requests are put in a list. Each elevator picks up requests from this list. An elevator request consists of origin floor and destination floor. An request that has been picked up by an elevator,  is removed from the list.


