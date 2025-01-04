namespace Tactics.Units.Services
{
    public interface IUnitMovement
    {
        void CalculatePath();
        void PerformMovementAsync(); // TODO import UniTask
    }
}