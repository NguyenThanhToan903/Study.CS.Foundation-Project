using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/List Rabbit Variable")]
public class ListRabbitVariable : ScriptableObject
{
    public List<RabbitMovement> rabbitMovement = new List<RabbitMovement>();
}
