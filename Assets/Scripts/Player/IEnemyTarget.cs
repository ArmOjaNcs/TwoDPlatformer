using UnityEngine;

public interface IEnemyTarget 
{
    public Vector3 Position { get; }

    public PlayerMover Player { get; }
}
