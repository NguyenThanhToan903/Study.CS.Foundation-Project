using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boundary")]
public class Boundary : ScriptableObject
{
    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;

    public static Boundary instance;

    public void Awake()
    {
        instance = this;
    }
}
