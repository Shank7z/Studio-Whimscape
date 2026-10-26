using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EntityBaseStats", menuName = "Scriptable Objects/Stats/EntityBaseStats")]
public class EntityBaseStats : ScriptableObject
{
    [SerializeField]
    public Dictionary<string, Stat> StatList = new Dictionary<string, Stat>();
}