using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IMoveSpeedComponent
    {
        float MoveSpeed { get; }
        void SetMoveSpeed(float moveSpeed);
    }

    public sealed class MoveSpeedComponent : IMoveSpeedComponent
    {
        public float MoveSpeed => _moveSpeed;

        private AtomicVariable<float> _moveSpeed;

        public MoveSpeedComponent(AtomicVariable<float> moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed.Value = moveSpeed;
        }
    }
}