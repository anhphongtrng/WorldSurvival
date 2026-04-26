using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] protected float maxHP = 5f;
    [SerializeField] protected float currentHP;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHP -= dmg;
    }

    public virtual float GetCurrentHP()
    {
        return currentHP;
    }

    public virtual float GetMaxHP()
    {
        return maxHP;
    }

    public virtual void SetHP(float newHp)
    {
        currentHP = Mathf.Clamp(newHp, 0, maxHP);
    }

    public virtual void HealHP(float amount)
    {
        if (IsDead()) return;

        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, -100, maxHP);
    }

    public virtual bool IsDead()
    {
        return currentHP <= 0;
    }
}