using UnityEngine;
using System.Collections.Generic;

public enum StatTypes
{
    hp,
    mp,
    attack,
    defense,
    luck,
    speed,
    expToNextLevel
}

[CreateAssetMenu(fileName = "StatsByLevel", menuName = "Scriptable Objects/Stats/StatsByLevel")]
public class StatsByLevel : ScriptableObject
{
    [SerializeField] public Dictionary<StatTypes, float[]> Stats;

    public float GetStat(StatTypes type, int level)
    {
        Stats.TryGetValue(type, out var chosenArray);
        return chosenArray[level - 1];
    }
}
