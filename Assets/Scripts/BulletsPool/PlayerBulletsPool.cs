using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletsPool : BulletsPool
{
    [SerializeField] private PlayerBullet _prefab;

    private ObjectPool<PlayerBullet> _pool;

    private protected override void Awake()
    {
        base.Awake();

        _pool = new ObjectPool<PlayerBullet>(
            createFunc: () => CreateBullet(),
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => DestroyObjectInPool(bullet),
            collectionCheck: true,
            defaultCapacity: Capacity,
            maxSize: MaxSize);
    }

    public PlayerBullet GetBullet()
    {
        return _pool.Get();
    } 

    private PlayerBullet CreateBullet()
    {
        PlayerBullet createdBullet = Instantiate(_prefab);
        createdBullet.Init(LifeTime, true);
        createdBullet.InitializeRigidbody();
        createdBullet.TargetReached += OnTargetReached;
        createdBullet.gameObject.SetActive(false);
        return createdBullet;
    }

    private void OnTargetReached(Bullet playerBullet)
    {
        _pool.Release((PlayerBullet)playerBullet);
    }

    private void DestroyObjectInPool(PlayerBullet bullet)
    {
        bullet.TargetReached -= OnTargetReached;
        Destroy(bullet.gameObject);
    }
}