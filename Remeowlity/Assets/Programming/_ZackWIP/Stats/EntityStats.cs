using UnityEngine;
using System.Collections.Generic;

public class EntityStats : MonoBehaviour, IDamageable
{
    public StatsByLevel BaseStatSheet;
    [SerializeField] public Dictionary<StatTypes, Stat> CurrentStats = new Dictionary<StatTypes, Stat>();

    private void Start()
    {
        FullSetup();
    }

    private void Setup(StatTypes stat)
    {
        BaseStatSheet.Stats.TryGetValue(stat, out var baseValueArray);
        CurrentStats.Add(stat, new Stat());
        CurrentStats[stat].Set(baseValueArray[currentLevel-1]);
    }

    private void FullSetup()
    {
        Setup(StatTypes.hp);
        Setup(StatTypes.mp);
        Setup(StatTypes.attack);
        Setup(StatTypes.defense);
        Setup(StatTypes.luck);
        Setup(StatTypes.speed);
        Setup(StatTypes.expToNextLevel);
    }

    #region Levels and Exp

    [CustomHeader("Level and Exp", 18)]
    [Space(10)]
    [Range(1, 30)]
    public int currentLevel = 1;
    [Min(0)]
    public float currentExp = 0;
    public void GainExp(float exp) // Adds the given amount of exp to currentExp
    {
        currentExp += exp;
        while (currentExp > BaseStatSheet.GetStat(StatTypes.expToNextLevel, currentLevel)) // Repeats the level up process until current xp is not enough to reach the next level
        {
            float overflowExp = currentExp - BaseStatSheet.GetStat(StatTypes.expToNextLevel, currentLevel);
            IncreaseLevel(1);
            currentExp = overflowExp;
        }
    }

    public void IncreaseLevel(int count) // Increments currentLevel by the given amount
    {
        currentLevel += count;
        FullSetup();
    }

    #endregion

    #region Health
    [CustomHeader("Health", 18)]
    [SerializeField] private float temp;
    public void TakeDamage(float damage)
    {
        CurrentStats[StatTypes.hp].Decrease(damage);
    }

    #endregion
}