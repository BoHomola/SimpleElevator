namespace SimpleElevator;

public class OnePersonElevatorRequest(UserData userData, int toFloor) : IElevatorRequest
{
    public UserData UserData { get; set; } = userData;
    public int ToFloor { get; set; } = toFloor;
    public int FromFloor => userData.CurrentFloor;

    public Direction Direction =>
        UserData.CurrentFloor < ToFloor ? Direction.Up : Direction.Down;
}