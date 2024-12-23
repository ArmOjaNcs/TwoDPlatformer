public interface IDetectionZoneListener
{ 
    public void OnPlayerFounded(IEnemyTarget player);
    public void OnTargetInZone();
    public void OnLostTarget();
}
