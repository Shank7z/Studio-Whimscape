using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float currentValue;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    // All runtime methods that allow adjustment of the currentValue
    #region Stat Adjustment Functions
    public void Increase(float amount) // Add amount to currentValue
    {
        currentValue = Mathf.Clamp(currentValue + amount, maxValue, minValue);
    }
    public void Decrease(float amount) // Subtract amount from currentValue
    {
        currentValue = Mathf.Clamp(currentValue - amount, minValue, maxValue);
    }
    public  void Multiply(float amount) // Multiply currentValue by amount
    {
        currentValue = Mathf.Clamp(currentValue * amount, minValue, maxValue);
    }
    public void Divide(float amount) // Divide currentValue by amount
    {
        currentValue = Mathf.Clamp(currentValue  / amount, minValue, maxValue);
    }
    public void Set(float amount) // Set currentValue to amount
    {
        currentValue = amount;
    }
    #endregion


}
