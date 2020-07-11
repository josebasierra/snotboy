namespace Interfaces
{
    public interface IControllable
    {
        void TakeControl(IControllable other);
        void SurrenderControl();

        void Enable();
        void Disable();

    }
}