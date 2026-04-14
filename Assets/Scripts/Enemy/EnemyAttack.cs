using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] protected float attackCooldown;

    protected float lastAttackTime;

    protected virtual void Awake()
    {
        attackCooldown = 2f;
        lastAttackTime = 0f;
    }

    protected virtual void Update()
    {

    }

    public virtual void Attack()
    {
        if(Time.time >= lastAttackTime + attackCooldown)
        {
            OnAttack();
            lastAttackTime = Time.time;
        }
    }

    protected abstract void OnAttack();
}