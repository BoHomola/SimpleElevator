namespace SimpleElevator;

public class OnePersonElevator(int minFloor, int maxFloor) : IElevator<OnePersonElevatorRequest>
{
    private List<OnePersonElevatorRequest> Requests { get; set; } = [];
    private OnePersonElevatorRequest? CurrentRequest { get; set; } = null;
    private readonly int minFloor = minFloor;
    private readonly int maxFloor = maxFloor;
    private int currentFloor = minFloor;


    public void AddRequest(OnePersonElevatorRequest elevatorRequest)
    {
        if (elevatorRequest.FromFloor < minFloor || elevatorRequest.FromFloor > maxFloor)
        {
            Console.WriteLine("Request from floor " + elevatorRequest.FromFloor + " is out of bounds");
            return;
        }

        Console.WriteLine("Received request by " + elevatorRequest.UserData.Username + " from " + elevatorRequest.FromFloor + " to " + elevatorRequest.ToFloor);
        Requests.Add(elevatorRequest);
    }

    public void Step(int numberOfSteps)
    {
        for (int i = 0; i < numberOfSteps; i++)
        {
            Step();
        }
    }

    public void Step()
    {
        // No current request and no more requests -> Idling
        if (CurrentRequest == null && Requests.Count == 0)
        {
            Console.WriteLine("Elevator is idle at floor " + currentFloor);
            return;
        }

        // No current request -> Move elevator toward next request and start to process it
        if (CurrentRequest == null)
        {
            ApproachNewRequest();
            return;
        }

        // Current request is finished
        if (CurrentRequest.ToFloor == currentFloor)
        {
            FinishElevatorRequest();
            return;
        }

        // Move the elevator in the direction of the current request
        MoveDirection(CurrentRequest.Direction);
    }

    private void ApproachNewRequest()
    {
        OnePersonElevatorRequest nextElevatorRequest = Requests.First();
        if (nextElevatorRequest.FromFloor == currentFloor)
        {
            Console.WriteLine("Elevator started processing request by " + nextElevatorRequest.UserData.Username + " from " + nextElevatorRequest.FromFloor + " to " +
                              nextElevatorRequest.ToFloor);
            CurrentRequest = nextElevatorRequest;
            return;
        }

        Direction nextDirection = nextElevatorRequest.FromFloor > currentFloor ? Direction.Up : Direction.Down;

        //Check if there is another request between the first request's floor and the current floor with the same direction
        OnePersonElevatorRequest? potentialRequest =
            Requests.FirstOrDefault(x => x.FromFloor == currentFloor && x.Direction == nextDirection);
        if (potentialRequest != null)
        {
            switch (nextDirection)
            {
                case Direction.Up when potentialRequest.ToFloor <= nextElevatorRequest.FromFloor:
                    Console.WriteLine("Elevator started processing request from " + potentialRequest.FromFloor +
                                      " to " +
                                      potentialRequest.ToFloor + " by " + potentialRequest.UserData.Username +
                                      " without limiting first request in line by " + nextElevatorRequest.UserData.Username + ", since it is on the way up.");
                    CurrentRequest = potentialRequest;
                    return;
                case Direction.Down when potentialRequest.ToFloor >= nextElevatorRequest.FromFloor:
                    Console.WriteLine("Elevator started processing request from " + potentialRequest.FromFloor +
                                      " to " +
                                      potentialRequest.ToFloor + " by " + potentialRequest.UserData.Username +
                                      " without limiting first request in line by " + nextElevatorRequest.UserData.Username + ", since it is on the way down.");
                    CurrentRequest = potentialRequest;
                    return;
            }
        }

        MoveDirection(nextDirection);
    }

    private void MoveDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                currentFloor++;
                Console.WriteLine("Elevator has gone one floor up to " + currentFloor);
                break;
            case Direction.Down:
                currentFloor--;
                Console.WriteLine("Elevator has gone one floor down to " + currentFloor);
                break;
        }
    }

    private void FinishElevatorRequest()
    {
        Console.WriteLine(
            "Elevator has finished request of " + CurrentRequest.UserData.Username + " from " + CurrentRequest.FromFloor + " to " + CurrentRequest.ToFloor);
        CurrentRequest.UserData.CurrentFloor = currentFloor;
        Requests.Remove(CurrentRequest);
        CurrentRequest = null;
    }
}