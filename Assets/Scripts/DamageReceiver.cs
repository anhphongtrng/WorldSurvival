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

    public virtual float GetHP()
    {
        return currentHP;
    }

    public virtual void SetHP(float newHp)
    {
        currentHP = Mathf.Clamp(newHp, 0, maxHP);
    }

    public virtual bool IsDead()
    {
        return currentHP <= 0;
    }
}