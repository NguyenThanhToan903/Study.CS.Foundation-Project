using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundary")]
public class Boundary : ScriptableObject
{
    [SerializeField] public Vector2 PointA;
    [SerializeField] public Vector2 PointB;
    [SerializeField] public Vector2 Center;

    [SerializeField] public float Radius;

}
