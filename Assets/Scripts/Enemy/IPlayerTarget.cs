using UnityEngine;

public interface IPlayerTarget 
{
   public Health Health { get; set; }
   public Vector3 Position { get; }
}
