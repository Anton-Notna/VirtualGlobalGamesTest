using DisposableSubscriptions;
using OmicronDamages;

namespace Game.Damages
{
    public interface IHealth : IDamageable<Damage>
    {
        public int Current { get; }

        public int Max { get; }

        public IUpdatable<IHealth> AsUpdatable { get; }
    }
}