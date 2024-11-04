using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundary")]
public class Boundary : ScriptableObject
{
    [SerializeField] public Vector2 PointA;
    [SerializeField] public Vector2 PointB;
    [SerializeField] public Vector2 Center;

    [SerializeField] public float Radius { private set; get; }

    private void Awake()
    {
        Radius = Mathf.Min(Mathf.Abs(PointA.x - PointB.x), Mathf.Abs(PointA.y - PointB.y));
    }
}
