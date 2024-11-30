public struct ShotData
{
    public bool IsShooting { get; private set; }
    public bool IsRightDirection {  get; private set; }
    public bool IsDucking { get; private set; }

    public ShotData(bool  isShooting, bool isRightDirection, bool isDucking)
    {
        IsShooting = isShooting;
        IsRightDirection = isRightDirection;
        IsDucking = isDucking;
    }
}