using SimpleElevator;

var elevator = new OnePersonElevator(1, 10);

var user1 = new UserData(1, "Jan");
var user2 = new UserData(4, "Filip");
var user3 = new UserData(1, "Marek");

elevator.Step(1);
elevator.AddRequest(new OnePersonElevatorRequest(user1,6));
elevator.Step(5);
elevator.AddRequest(new OnePersonElevatorRequest(user2,8));
elevator.Step(8);
elevator.AddRequest(new OnePersonElevatorRequest(user3,9));
elevator.Step(2);
elevator.AddRequest(new OnePersonElevatorRequest(user1,2));
elevator.Step(30);
