using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundary")]
public class Boundary : ScriptableObject
{
    [SerializeField] public Vector2 PointA;
    [SerializeField] public Vector2 PointB;
    [SerializeField] public Vector2 Center;

    [SerializeField] public float Radius;

    //private void Awake()
    //{
    //    Radius = Mathf.Min(Mathf.Min(Mathf.Abs(PointA.x), Mathf.Abs(PointA.y)), Mathf.Min(Mathf.Abs(PointB.x), Mathf.Abs(PointB.y)));
    //    Center = (PointA + PointB) / 2;
    //}



}
