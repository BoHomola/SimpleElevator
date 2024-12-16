namespace SimpleElevator;

public class UserData(int currentFloor, string username)
{
    public int CurrentFloor { get; set; } = currentFloor;
    public string Username { get; set; } = username;
}