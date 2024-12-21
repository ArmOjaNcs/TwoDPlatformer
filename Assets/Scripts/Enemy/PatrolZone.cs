using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PatrolZone : MonoBehaviour
{
    public Vector3 Position => transform.position;
}