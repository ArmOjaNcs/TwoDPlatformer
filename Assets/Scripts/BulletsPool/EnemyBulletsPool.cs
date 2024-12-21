using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletsPool : BulletsPool
{
    [SerializeField] private EnemyBullet _prefab;

    private ObjectPool<EnemyBullet> _pool;

    private protected override void Awake()
    {
        base.Awake();

        _pool = new ObjectPool<EnemyBullet>(
            createFunc: () => CreateBullet(),
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => DestroyObjectInPool(bullet),
            collectionCheck: true,
            defaultCapacity: Capacity,
            maxSize: MaxSize);
    }

    public EnemyBullet GetBullet()
    {
        return _pool.Get();
    }

    private EnemyBullet CreateBullet()
    {
        EnemyBullet createdBullet = Instantiate(_prefab);
        createdBullet.Init(LifeTime, false);
        createdBullet.TargetReached += OnTargetReached;
        createdBullet.gameObject.SetActive(false);
        return createdBullet;
    }

    private void OnTargetReached(Bullet bullet)
    {
        _pool.Release((EnemyBullet)bullet);
    }

    private void DestroyObjectInPool(EnemyBullet bullet)
    {
        bullet.TargetReached -= OnTargetReached;
        Destroy(bullet.gameObject);
    }
}