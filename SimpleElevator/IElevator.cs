namespace SimpleElevator;

public interface IElevator<TRequest> where TRequest : IElevatorRequest
{
    public void Step();
    public void Step(int numberOfSteps);
    public void AddRequest(TRequest request);
}