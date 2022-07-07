namespace Script.Base.Fighting
{
    public interface IDamageable
    {
        int HitPoints { get; }

        void TakeDamage(Damage damage);
    }
}