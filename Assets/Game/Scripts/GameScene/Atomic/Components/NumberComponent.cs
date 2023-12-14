using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface INumberComponent
    {
        int NubmerAmmo { get; set; }
    }

    public class AmmoContainerComponent : INumberComponent
    {
        public int NubmerAmmo { get => _nubmerAmmo.Value; set => _nubmerAmmo.Value = value; }
        private AtomicVariable<int> _nubmerAmmo = new();

        public AmmoContainerComponent(AtomicVariable<int> nubmerAmmo)
        {
            _nubmerAmmo = nubmerAmmo;
        }
    }
}