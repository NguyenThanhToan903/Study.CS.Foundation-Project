using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundary")]
public class Boundary : ScriptableObject
{
    public Vector2 PointA;
    public Vector2 PointB;

    public float Radius;
    public Vector2 Center => (PointA + PointB) / 2f;
    public Vector2 Size => new Vector2(Mathf.Abs(PointB.x - PointA.x), Mathf.Abs(PointB.y - PointA.y));

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(Center, Size);
    }
}
