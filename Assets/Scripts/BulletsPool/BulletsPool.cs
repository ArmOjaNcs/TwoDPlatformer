using UnityEngine;

public abstract class BulletsPool : MonoBehaviour
{
    [SerializeField] private protected int Capacity;
    [SerializeField] private protected int MaxSize;
    [SerializeField] private protected float BulletLifeTime;

    private protected WaitForSeconds LifeTime;

    private protected virtual void Awake()
    {
        LifeTime = new WaitForSeconds(BulletLifeTime);
    }
}