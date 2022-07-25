
using UnityEngine;
using Lore.Stats;
using System;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public Stat Intelligence;
    public Stat Stealth;
    public Stat Bargaining;
    public Stat MoveSpeed;

    [Header("Currencies")]
    private float _gold;
    private float _reputation;
    public float Gold { get { return _gold; } set { _gold = value; } }
    public float Reputation { get { return _reputation; } set { _reputation = value; } }

    // Events
    public Action<float> onGoldChange;
    public Action<float> onReputationChange;



    public void GiveGold(float amount)
    {
        amount = Mathf.Abs(amount);
        Gold += amount;
        onGoldChange?.Invoke(amount);
    }

    public void LoseGold(float amount)
    {
        amount = Mathf.Abs(amount);
        Gold -= amount;
        onGoldChange?.Invoke(-amount);
    }

    public void GiveReputation(float amount)
    {
        amount = Mathf.Abs(amount);
        Reputation += amount;
        onReputationChange?.Invoke(amount);
    }

    public void LoseReputation(float amount)
    {
        amount = Mathf.Abs(amount);
        Reputation -= amount;
        onReputationChange?.Invoke(-amount);
    }


    public Stat GetStat(StatType stattype)
    {
        switch (stattype)
        {
            case StatType.Intelligence:
                return this.Intelligence;
            case StatType.Stealth:
                return this.Stealth;
            case StatType.MoveSpeed:
                return this.MoveSpeed;
            case StatType.Bargaining:
                return this.Bargaining;
            default:
                throw new UnityException("Stat not defined for player");
        }
    }
}
