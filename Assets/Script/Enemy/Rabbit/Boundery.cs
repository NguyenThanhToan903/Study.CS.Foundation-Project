using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundary")]
public class Boundary : ScriptableObject
{
    [SerializeField] private float radius = 10f;

    public float Radius => radius;


    public Vector3 Center { get; set; }
}
