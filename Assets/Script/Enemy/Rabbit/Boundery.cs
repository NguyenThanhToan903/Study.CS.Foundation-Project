using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundary")]
public class Boundary : ScriptableObject
{
    [SerializeField] private float radius = 10f;

    public float Radius => radius;

    [SerializeField] private float size = 5f;

    public float Size => size;

    public Vector3 Center { get; set; }
}
