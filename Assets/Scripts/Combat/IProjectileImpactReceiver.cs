public interface IProjectileImpactReceiver
{
    ProjectileSide DamagingSide { get; }

    void ReceiveProjectileImpact();
}