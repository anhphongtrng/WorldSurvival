using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] protected float hp;

    void Start()
    {
        hp = 3;
    }

    public virtual void TakeDamage(float dmg)
    {
        hp -= dmg;
    }

    public float GetHP()
    {
        return hp;
    }

    public virtual void SetHP(float newHp)
    {
        hp = newHp;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }
}