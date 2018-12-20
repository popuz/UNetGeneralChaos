namespace UNetGeneralChaos
{
    public interface IUnitMovement
    {
        bool UseTick { get; }

        void Init(IPlayerInput playerInput);
        void Tick();
        void Tick(float fixedDeltaTime);
    }
}
